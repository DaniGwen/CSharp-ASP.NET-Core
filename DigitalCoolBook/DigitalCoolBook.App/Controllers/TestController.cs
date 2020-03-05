namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(string id)
        {
            var tests = await this.testService.GetTestAsync(id);

            var model = new TestViewModel
            {
                Grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
                TestId = id,
            };

            return this.View(model);
        }

        // Teacher adds students, sets the timer and start the test
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(TestViewModel model, string[] chkBox)
        {
            // Checks if no students has been selected
            if (chkBox.Length == 0)
            {
                this.ModelState.AddModelError(string.Empty, "Добавете поне един ученик.");
                return this.View(model);
            }

            try
            {
                // Create and map test
                var test = this.mapper.Map<Test>(model);

                // Adding Id to test
                test.TestId = Guid.NewGuid().ToString();

                // Finds current Teacher Id
                test.TeacherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                test.TestName = model.TestTitle;

                // Adding "TestStudent" relation  between Test and Student
                foreach (var student in chkBox)
                {
                    var testStudent = new TestStudent()
                    {
                        StudentId = this.userService.GetStudents().FirstOrDefault(s => s.Name == student).Id,
                        TestId = test.TestId,
                    };

                    test.TestStudent.Add(testStudent);
                }

                await this.testService.AddTestAsync(test);
            }
            catch (Exception exception)
            {
                return this.View("/Error", exception.Message);
            }

            return this.RedirectToAction("/StartTest");
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

            return this.Redirect("/Home/Success");
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

        [HttpGet]
        [Authorize(Roles = "Teacher")]
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

            model.Timer = test.Timer.ToString();

            return this.View(model);
        }

        [HttpPost]
        [ActionName("EndTest")]
        public async Task<IActionResult> EndTestAsync(TestStartViewModel model)
        {
            // REFACTOR AND IMPLEMENT
            var test = await this.testService
                .GetTestAsync(this.TempData["TestId"].ToString());

            test.Date = DateTime.Now;
            test.IsExpired = true;

            var expiredTest = this.mapper.Map<ExpiredTest>(test);

            this.ViewData["SuccessMsg"] = "Теста беше предаден.";
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
            //TODO 


            return this.Json(string.Empty);
        }
    }
}