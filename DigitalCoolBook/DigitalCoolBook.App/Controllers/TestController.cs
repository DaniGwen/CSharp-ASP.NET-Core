namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.CategoryViewModels;
    using DigitalCoolBook.App.Models.TestviewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly IQuestionService questionService;

        public TestController(
            ITestService testService,
            ISubjectService subjectService,
            IMapper mapper,
            IGradeService gradeService,
            IUserService userService,
            IQuestionService questionService)
        {
            this.testService = testService;
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.gradeService = gradeService;
            this.userService = userService;
            this.questionService = questionService;
        }

        // Admin creates a test for a lesson
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTestAdmin()
        {
            var lessons = this.subjectService.GetLessons().ToList();

            var model = new TestCreateAdminViewModel()
            {
                Lessons = this.mapper.Map<List<LessonsViewModel>>(lessons),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("CreateTestAdmin")]
        public async Task<IActionResult> CreateTestAdminAsync(ICollection<QuestionAnswerViewModel> model, string LessonId, string place)
        {
            var lesson = await this.subjectService.GetLessonAsync(LessonId);

            // Instanciate test
            var test = new Test
            {
                TestId = Guid.NewGuid().ToString(),
                LessonId = LessonId,
                Place = place,
                TestName = lesson.Title,
            };

            // Collections of answers and questions to be saved in DB
            var questionsForDb = new List<Question>();
            var answersForDb = new List<Answer>();

            // Create question and asnwers
            foreach (var questionDto in model)
            {
                var question = new Question
                {
                    QuestionId = Guid.NewGuid().ToString(),
                    TestId = test.TestId,
                    Title = questionDto.Question,
                };

                questionsForDb.Add(question);

                // Setting answers properties
                foreach (var answerDto in questionDto.Answers)
                {
                    var answer = new Answer
                    {
                        AnswerId = Guid.NewGuid().ToString(),
                        Title = answerDto,
                        QuestionId = question.QuestionId,
                    };

                    answersForDb.Add(answer);
                }
            }

            await this.testService.AddTestAsync(test);
            await this.questionService.AddQuestionsAsync(questionsForDb);
            await this.questionService.AddAnswersAsync(answersForDb);

            return this.RedirectToAction("MarkCorrectAnswers", new { testId = test.TestId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult MarkCorrectAnswers(string testId)
        {
            var questions = this.questionService
                .GetQuestions()
                .Where(question => question.TestId == testId);

            // Map questions to question model
            var model = this.mapper.Map<List<QuestionsModel>>(questions);

            // Passing test Id to this view
            this.TempData["TestId"] = testId;

            // Add answers to questions
            foreach (var question in model)
            {
                question.Answers = this.questionService.GetAnswers()
               .Where(answer => answer.QuestionId == question.QuestionId)
               .ToList();
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("MarkCorrectAnswers")]
        public async Task<IActionResult> MarkCorrectAnswersAsync(string[] correctAnswerIds, string testId)
        {
            try
            {
                // Sets correct answers to true
                foreach (var correctAnswer in correctAnswerIds)
                {
                    var answer = await this.questionService.GetAnswerAsync(correctAnswer);
                    answer.IsCorrect = true;
                }

                await this.questionService.SaveChangesAsync();

                return this.Json("Успешно записване.");
            }
            catch (Exception exception)
            {
                // return error object to view
                return this.Json(new { error = "Нещо се обърка.", message = exception.Message });
            }
        }

        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("GetStudents")]
        public JsonResult GetStudentsAsync(string gradeId)
        {
            var students = this.userService.GetStudents()
                .Where(s => s.GradeId == gradeId)
                .ToList();

            return this.Json(students);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Tests()
        {
            var tests = this.testService.GetTests().ToList();

            // Map and create model for the view
            var model = this.mapper.Map<List<TestsNamesViewModel>>(tests);

            return this.View(model);
        }

        // Set the timer for the test before it starts
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        [ActionName("SetTestTimer")]
        public async Task<IActionResult> SetTestTimerAsync(string testId)
        {
            var model = new TestViewModel
            {
                Grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
                TestId = testId,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ActionName("SetTestTimer")]
        public async Task<IActionResult> SetTestTimerAsync(TestViewModel model, string[] students)
        {
            try
            {
                if (students.Length == 0)
                {
                    this.ModelState.AddModelError(string.Empty, "Добавете поне един ученик.");
                    return this.View(model);
                }

                // Get test from DB
                var test = await this.testService.GetTestAsync(model.TestId);

                // Finds current Teacher Id
                test.TeacherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                // Set test timer
                test.Timer = model.Timer;

                // Adding test-student in list before adding them to DB
                var testStudentForDB = new List<TestStudent>();

                // Adding "TestStudent" relation  between Test and Student
                foreach (var student in students)
                {
                    var testStudent = new TestStudent()
                    {
                        StudentId = this.userService.GetStudents().FirstOrDefault(s => s.Name == student).Id,
                        TestId = test.TestId,
                    };

                    testStudentForDB.Add(testStudent);
                }

                // Add mapping entity test-student to DB
                await this.testService.AddTestStudentsAsync(testStudentForDB);
                await this.testService.SaveChangesAsync();

                return this.RedirectToAction("Tests");
            }
            catch (Exception exception)
            {
                this.TempData["ErrorMsg"] = exception.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public async Task<IActionResult> StartTest(string id)
        {
            var test = await this.testService.GetTestAsync(id);

            this.TempData["TestId"] = test.TestId;

            // Map test to testModel
            var model = this.mapper.Map<TestStartViewModel>(test);

            try
            {
                // Gets the questions for this test
                var questionsDb = this.questionService
               .GetQuestions()
               .Where(question => question.TestId == test.TestId)
               .ToList();

                // Add questions to model and map questions to QuestionModel
                model.Questions
                        .AddRange(this.mapper.Map<List<QuestionsModel>>(questionsDb));
            }
            catch (Exception exception)
            {
                this.View("/Error", exception.Message);
            }

            try
            {
                // Add Answers to Questions for this test
                foreach (var question in model.Questions)
                {
                    var answers = this.questionService.GetAnswers()
                        .Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    question.Answers.AddRange(answers);
                }
            }
            catch (Exception exception)
            {
                this.View("/Error", exception.Message);
            }

            return this.View(model);
        }

        [HttpPost]
        [ActionName("EndTest")]
        public async Task<IActionResult> EndTestAsync(ICollection<EndTestViewModel> model)
        {
            // Gets questions for this test
            var questions = this.questionService
                .GetQuestions()
                .Include(question => question.Answers)
                .Where(question => question.TestId == this.TempData["TestId"].ToString())
                .ToList();

            var points = 0;

            // Filter correct answers relevant to these questions
            foreach (var question in questions)
            {
                var correctAnswer = question.Answers
                    .First(answer => answer.IsCorrect == true);

                var modelAnswer = model
                    .First(model => model.QuestionId == question.QuestionId || model.AnswerId == correctAnswer.AnswerId);

                if (modelAnswer != null)
                {
                    points += 1;
                }
            }

            var test = await this.testService
                .GetTestAsync(this.TempData["TestId"].ToString());

            test.Date = DateTime.Now;

            var expiredTest = this.mapper.Map<ExpiredTest>(test);

            // Add expired test to DB
            await this.testService.AddExpiredTestAsync(expiredTest);

            // TODO Check against DB if marked answers are correct and calculate result
            this.TempData["SuccessMsg"] = "Теста беше предаден.";
            return this.Redirect("/Home/Success");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddQuestions()
        {
            var lessonsDb = this.subjectService.GetLessons()
                .OrderBy(lesson => lesson.Title)
                .ToList();

            var lessonsDto = this.mapper.Map<List<LessonsViewModel>>(lessonsDb);

            var model = new QuestionsAddViewModel
            {
                Lessons = lessonsDto,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("AddQuestions")]
        public async Task<IActionResult> AddQuestionsAsync(QuestionsAddViewModel model)
        {
            try
            {
                if (model.Questions.Count < 1)
                {
                    this.ModelState.AddModelError(string.Empty, "Моля добавете поне един въпрос.");
                    return this.View(model);
                }

                var test = this.testService.GetTests()
                    .Include("Lesson")
                    .FirstOrDefault(t => t.LessonId == model.LessonId);

                foreach (var question in model.Questions)
                {
                    test.Questions.Add(new Question
                    {
                        QuestionId = Guid.NewGuid().ToString(),
                        Title = question,
                    });
                }
            }
            catch (Exception exception)
            {
                this.TempData["ErrorMsg"] = exception.Message;

                return this.Redirect("/Home/ErrorView");
            }

            await this.testService.SaveChangesAsync();
            this.TempData["SuccessMsg"] = "Въпросите бяха записани";

            return this.Redirect("/Home/Success");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CheckTestExist(string lessonId)
        {
            var test = this.testService.GetTests()
               .FirstOrDefault(t => t.LessonId == lessonId);

            if (test == null)
            {
                return this.BadRequest();
            }

            return this.Json(string.Empty);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLessons(string lessonId)
        {
            // TODO 

            return this.Json(string.Empty);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult TestsPreview()
        {
            var tests = this.testService.GetTests();

            var model = this.mapper.Map<List<TestPreviewViewModel>>(tests);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("GetTestDetails")]
        public async Task<IActionResult> GetTestDetailsAsync(string id)
        {
            try
            {
                var test = await this.testService.GetTestAsync(id);

                // Map test to view model
                var model = this.mapper.Map<TestDetailsViewModel>(test);

                // Getting the questions for this test
                var questions = this.questionService.GetQuestions()
                    .Where(question => question.TestId == model.TestId)
                    .ToList();

                // Getting the answers from DB
                var answers = this.questionService.GetAnswers().ToList();

                // Map the questions to questions view model
                var questionsModel = this.mapper.Map<List<QuestionDetailsViewModel>>(questions);

                // Iterate through questionsModel and add answers
                foreach (var question in questionsModel)
                {
                    // Filter the answers for that question
                    var answersForQuestion = answers.Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    // Map answers to answers view model
                    var answersModel = this.mapper.Map<List<AnswerDetailsViewModel>>(answersForQuestion);

                    question.Answers = answersModel;
                }

                model.Questions = questionsModel;

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Json("Нещо се обърка.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("DeleteTest")]
        public async Task<IActionResult> DeleteTestAsync(string testId)
        {
            try
            {
                await this.testService.RemoveTestAsync(testId);

                return this.Json("Теста е изтрит.");
            }
            catch (Exception)
            {
                return this.Json("Грешка при изтриване на теста!");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("EditTest")]
        public async Task<IActionResult> EditTestAsync(string id)
        {
            try
            {
                var test = await this.testService.GetTestAsync(id);

                // Map test to view model
                var model = this.mapper.Map<TestDetailsViewModel>(test);

                // Getting the questions for this test
                var questions = this.questionService.GetQuestions()
                    .Where(question => question.TestId == model.TestId)
                    .ToList();

                // Getting the answers from DB
                var answers = this.questionService.GetAnswers().ToList();

                // Map the questions to questions view model
                var questionsModel = this.mapper.Map<List<QuestionDetailsViewModel>>(questions);

                // Iterate through questionsModel and add answers
                foreach (var question in questionsModel)
                {
                    // Filter the answers for that question
                    var answersForQuestion = answers.Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    // Map answers to answers view model
                    var answersModel = this.mapper.Map<List<AnswerDetailsViewModel>>(answersForQuestion);

                    question.Answers = answersModel;
                }

                model.Questions = questionsModel;

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Json("Нещо се обърка.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("EditTest")]
        public async Task<IActionResult> EditTestAsync(ICollection<TestEditViewModel> model, string testId)
        {
            try
            {
                // questions for test from DB
                var questions = this.questionService.GetQuestions()
                    .Where(q => q.TestId == testId)
                    .ToList();

                // answers from DB
                var answers = this.questionService.GetAnswers()
                    .ToList();

                var answersForDb = new List<Answer>();

                foreach (var question in questions)
                {
                    foreach (var questionModel in model)
                    {
                        // Set question title
                        if (question.QuestionId == questionModel.QuestionId)
                        {
                            question.Title = questionModel.Question;

                            // Find the answers belong to this question
                            var answersToRemove = answers
                                .Where(a => a.QuestionId == question.QuestionId)
                                .ToList();

                            // Remove old answers
                            await this.questionService.RemoveAnswers(answersToRemove);

                            // Create new answers with updated titles
                            foreach (var answerTitle in questionModel.Answers)
                            {
                                var answer = new Answer
                                {
                                    AnswerId = Guid.NewGuid().ToString(),
                                    QuestionId = question.QuestionId,
                                    Title = answerTitle,
                                };

                                answersForDb.Add(answer);
                            }
                        }
                    }
                }

                await this.questionService.AddAnswersAsync(answersForDb);

                // Redirect to MarkCorrectAnswers action to select the right answers
                return this.RedirectToAction("MarkCorrectAnswers", new { testId = testId });
            }
            catch (Exception)
            {
                this.TempData["ErrorMsg"] = "Грешка при обработка на заявката!";
                return this.View("/Home/Error");
            }
        }
    }
}