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

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(TestViewModel model, string[] chkBox)
        {
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
                this.TempData["ErrorMsg"] = exception.Message;
                return this.RedirectToAction("ErrorView", "Home");
            }

            this.TempData["SuccessMsg"] = "Теста е създаден успешно!";

            return this.RedirectToAction("/StartTest");
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
            var teacherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var tests = this.testService.GetTests().ToList();

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
            var testModel = this.mapper.Map<TestStartViewModel>(test);

            try
            {
                var questionsDb = this.questionService
               .GetQuestions()
               .Where(question => question.TestId == test.TestId)
               .ToList();

                // Add questions to model
                testModel.Questions
                        .AddRange(this.mapper.Map<List<QuestionsModel>>(questionsDb));

                if (testModel.Questions.Count == 0)
                {
                    this.TempData["ErrorMsg"] = "Теста не съдържа въпроси.";
                    this.Redirect("/Home/Error");
                }
            }
            catch (Exception exception)
            {
                this.View("/Error", exception.Message);
            }

            try
            {
                // Add Answers to testModel.Questions
                foreach (var question in testModel.Questions)
                {
                    var answers = this.questionService.GetAnswers()
                        .Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    if (answers.Count == 0)
                    {
                        this.TempData["ErrorMsg"] =
                            "Някой от въпросите нямат отговори. Моля добавете отговори за всеки въпрос.";

                        this.Redirect("Home/Error");
                    }

                    question.Answers.AddRange(answers);
                }
            }
            catch (Exception exception)
            {
                this.View("/Error", exception.Message);
            }

            testModel.IsExpired = false;
            testModel.Timer = test.Timer.ToString();

            return this.View(testModel);
        }

        [HttpPost]
        [ActionName("EndTest")]
        public async Task<IActionResult> EndTestAsync(TestStartViewModel model)
        {
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
                        Content = question,
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