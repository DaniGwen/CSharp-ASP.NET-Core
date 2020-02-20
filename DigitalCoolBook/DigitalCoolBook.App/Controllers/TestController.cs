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

        public TestController(
            ITestService testService,
            ISubjectService subjectService,
            IMapper mapper,
            IGradeService gradeService,
            IUserService userService)
        {
            this.testService = testService;
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.gradeService = gradeService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult CreateTest(string id)
        {
            var lessons = this.subjectService.GetLessons().ToList();

            var model = new TestViewModel
            {
                Grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
                LessonId = id,
                Lessons = this.mapper.Map<List<LessonsViewModel>>(lessons),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(TestViewModel model, string[] chkBox)
        {
            try
            {
                // Getting lesson from DB to create test Name
                var lesson = await this.subjectService.GetLessonAsync(model.LessonId);
                var testName = "Тест по:  " + lesson.Title;

                // Mapping and setting test
                var test = this.mapper.Map<Test>(model);
                test.TestId = Guid.NewGuid().ToString();
                test.TeacherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                test.TestName = testName;

                // Adding relation  between Test and Student
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

            return this.RedirectToAction("Success", "Home");
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
            var tests = this.testService.GetTests()
                .Where(t => t.TeacherId == teacherId)
                .ToList();

            var model = this.mapper.Map<List<TestsNamesViewModel>>(tests);
            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> StartTest(string id)
        {
            var test = await this.testService.GetTestAsync(id);
            test.Date = DateTime.Now;
            await this.testService.SaveChangesAsync();

            var model = this.mapper.Map<TestStartViewModel>(test);

            // for TEST only
            var questions = new List<QuestionsModel>
            {
                new QuestionsModel
                {
                    Name = "Koe ot slednite?",
                },
                new QuestionsModel
                {
                    Name = "Ima li .....",
                },
                new QuestionsModel
                {
                    Name = "V koe ot izbroenite?",
                },
            };
            model.Questions.AddRange(questions);
            model.Timer = string.Format("{0:s}", test.Timer);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult EndTest(TestStartViewModel model)
        {
            // TODO: Implement
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