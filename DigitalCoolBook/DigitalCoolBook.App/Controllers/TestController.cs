namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.TestviewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IGradeService gradeService;

        public TestController(
            ITestService testService,
            ISubjectService subjectService,
            IMapper mapper,
            IGradeService gradeService)
        {
            this.testService = testService;
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.gradeService = gradeService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> CreateTest(string id)
        {
            var model = new TestViewModel
            {
                Grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
                Date = DateTime.UtcNow,
                LessonId = id,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(TestViewModel model)
        {
            var test = new Test
            {
                TestId = Guid.NewGuid().ToString(),
                TeacherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            };
            this.mapper.Map<Test>(model);
            await this.testService.AddTestAsync(test);

            return this.View();
        }
    }
}