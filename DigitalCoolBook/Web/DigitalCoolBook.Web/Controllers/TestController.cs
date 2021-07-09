using Microsoft.EntityFrameworkCore;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Hubs;
    using Models.CategoryViewModels;
    using Models.TestviewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly IQuestionService questionService;
        private readonly IScoreService scoreService;
        private readonly IHubContext<TestHub> testHub;

        public TestController(
            ITestService testService,
            ISubjectService subjectService,
            IMapper mapper,
            IGradeService gradeService,
            IUserService userService,
            IQuestionService questionService,
            IScoreService scoreService,
            IHubContext<TestHub> testHub)
        {
            this.testService = testService;
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.gradeService = gradeService;
            this.userService = userService;
            this.questionService = questionService;
            this.scoreService = scoreService;
            this.testHub = testHub;
        }

        // Admin creates a test for a lesson
        [HttpGet]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult CreateTest()
        {
            var lessons = this.subjectService.GetLessons().ToList();

            var model = new TestCreateAdminViewModel()
            {
                Lessons = this.mapper.Map<List<LessonsViewModel>>(lessons),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(ICollection<QuestionAnswerViewModel> model,
            string lessonId,
            string place)
        {
            var lesson = await this.subjectService.GetLessonAsync(lessonId);

            // Instantiate test
            var test = new Test
            {
                TestId = Guid.NewGuid().ToString(),
                LessonId = lessonId,
                Place = place,
                TestName = lesson.Title,
            };

            // Collections of answers and questions to be saved in DB
            var questionsForDb = new List<Question>();
            var answersForDb = new List<Answer>();

            // Create question and answers
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

                return this.Json("Successfully saved.");
            }
            catch (Exception exception)
            {
                // return error object to view
                return this.Json(new { error = "Ops something gone wrong.", message = exception.Message });
            }
        }

        [Authorize(Roles = "Teacher, Admin")]
        public JsonResult GetStudents(string gradeId)
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
        public IActionResult SetTestTimer(string testId)
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
                    this.ModelState.AddModelError(string.Empty, "At least one student is needed.");

                    return this.View(model);
                }

                // Get test from DB
                var test = await this.testService.GetTestAsync(model.TestId);

                // Set current Teacher Id to test
                test.TeacherId = this.User
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                // Set test timer from input model
                test.Timer = model.Timer;

                // Adding TestStudent in list before adding them to DB
                var testStudentForDB = new List<TestStudent>();

                // Adding "TestStudent" relation
                foreach (var student in students)
                {
                    var testStudent = new TestStudent()
                    {
                        StudentId = this.userService
                        .GetStudents()
                        .FirstOrDefault(s => s.Name == student).Id,

                        TestId = test.TestId,
                    };

                    testStudentForDB.Add(testStudent);
                }

                // Adding students in TestRoom
                string testRoomId = await this.testService.AddTestRoomAsync(students, test.TeacherId, model.TestId);

                // Add entity testStudent to DB
                await this.testService.AddTestStudentsAsync(testStudentForDB);

                return this.RedirectToAction("StartTest", "Test", new { Id = test.TestId });
            }
            catch (Exception exception)
            {
                this.TempData["ErrorMsg"] = exception.Message;

                return this.Redirect("/Home/Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public async Task<IActionResult> StartTest(string id)
        {
            try
            {
                var test = await this.testService.GetTestAsync(id);

                // Keep the test Id for EndTest action
                this.TempData["TestId"] = test.TestId;

                // Map test to testModel
                var model = this.mapper.Map<TestStartViewModel>(test);

                // Gets the participating students
                model.StudentNames = await this.testService
                    .GetStudentsInTestRoomAsync(test.TestId);

                // Gets the questions for this test
                var questionsDb = this.questionService
               .GetQuestions()
               .Where(question => question.TestId == test.TestId)
               .ToList();

                // Add questions to model and map questions to QuestionModel
                model.Questions
                        .AddRange(this.mapper.Map<List<QuestionsModel>>(questionsDb));

                // Add Answers to Questions for this test
                foreach (var question in model.Questions)
                {
                    var answers = this.questionService.GetAnswers()
                        .Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    question.Answers.AddRange(answers);
                }

                return this.View(model);
            }
            catch (Exception exception)
            {
                this.TempData["ErrorMsg"] = exception.Message;

                this.Redirect("/Home/Error");
            }

            return this.View();
        }

        [HttpPost]
        [ActionName("EndTest")]
        [Authorize(Roles = "Teacher, Student")]
        public async Task<IActionResult> EndTestAsync(ICollection<EndTestViewModel> model)
        {
            int result = 0;

            var testId = this.TempData["TestId"].ToString();

            if (this.User.IsInRole("Student"))
            {
                result = await this.ProcessTestAsync(model, testId);
            }

            this.ViewData["Result"] = result;

            // Check if all students in the test room has finished
            bool isAllFinished = this.testService.CheckAllFinished();

            if (!isAllFinished)
            {
                // Test room is empty so we remove it along with the students in it
                await this.testService.RemoveTestRoomAsync(testId);
            }

            if (this.User.IsInRole("Teacher"))
            {
                return this.Redirect("/Home/Index");
            }

            return this.View("Result");
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

                var test = this.testService
                    .GetTests()
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
                return this.Json("Something gone wrong..");
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

                return this.Json("Test is deleted.");
            }
            catch (Exception)
            {
                return this.Json("Error deleting test!");
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
                return this.Json("Error editing test.");
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
                this.TempData["ErrorMsg"] = "Error editing test!";
                return this.View("/Home/Error");
            }
        }

        private async Task<int> ProcessTestAsync(ICollection<EndTestViewModel> model, string testId)
        {
            var studentId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Set the student status to finished in this test
            var testRoomStudent = this.testService.GetTestRoomStudent(studentId);
            testRoomStudent.Finished = true;

            // Gets questions for this test
            var questions = this.questionService
                .GetQuestions()
                .Include(question => question.Answers)
                .Where(question => question.TestId == testId)
                .ToList();

            // Max points 100
            var points = 0;

            // Check for correct answer and add points
            foreach (var question in questions)
            {
                var correctAnswerId = question.Answers
                    .First(answer => answer.IsCorrect == true).AnswerId;

                var modelAnswerId = model.First(q => q.QuestionId == question.QuestionId).AnswerId;

                if (modelAnswerId == correctAnswerId)
                {
                    points += 10;
                }
            }

            // Get Test from DB
            var test = await this.testService
                .GetTestAsync(testId);

            // Create expired test to keep history
            var expiredTest = this.mapper.Map<ExpiredTest>(test);
            expiredTest.Date = DateTime.UtcNow;
            expiredTest.Result = points;
            expiredTest.StudentId = studentId;

            // Add expired test to DB if score is bigger then the one in the database
            if (this.testService.GetExpiredTests().Any())
            {
                var expiredTestDb = this.testService.GetExpiredTests().FirstOrDefault();

                if (expiredTestDb?.Result < expiredTest.Result)
                {
                    await this.testService.RemoveExpiredTest(expiredTestDb.ExpiredTestId);
                    await this.testService.AddExpiredTestAsync(expiredTest);
                }
            }
            else
            {
                await this.testService.AddExpiredTestAsync(expiredTest);
            }

            // Create Score
            if (this.User.IsInRole("Student"))
            {
                // Create Score for student
                var scoreId = await this.scoreService.CreateScoreAsync(points, test.LessonId);

                // Create ScoreStudent
                await this.scoreService.CreateScoreStudentAsync(scoreId, studentId);
            }

            // Set the student score property and display it after all students has finished in teacher's view
            testRoomStudent.Score = points;

            return points;
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult IsStudentInTest()
        {
            var studentId = this.User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            var testId = this.testService.IsStudentInTest(studentId);

            var testRoomStudent = this.testService.GetTestRoomStudent(studentId);

            if (testId != null && testRoomStudent.Finished == false)
            {
                var testName = this.testService
                    .GetTests()
                    .First(x => x.TestId == testId)
                    .TestName;

                return this.Json(new
                {
                    success = true,
                    TestId = testId,
                    TestName = testName,
                });
            }

            return this.Json(new
            {
                success = false,
                Message = "No active tests.",
            });
        }

        [Authorize(Roles = "Teacher")]
        [ActionName("EndTestAllStudents")]
        public async Task<IActionResult> EndTestAllStudentsAsync(string id)
        {
            await this.testHub.Clients.All.SendAsync("SubmitAll");

            var testId = this.testService
                .GetTests()
                .First(x => x.TestName == id)
                .TestId;

            await this.testService.RemoveTestRoomAsync(testId);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult ActiveTests()
        {
            var teacherId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<Test> activeTests = this.testService.GetActiveTestsByTeacherId(teacherId);

            var model = this.mapper.Map<List<ActiveTestsViewModel>>(activeTests);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult TestsHistory()
        {
            var teacherId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tests = this.testService.GetExpiredTestsByTeacherId(teacherId);

            return this.View(tests);
        }
    }
}