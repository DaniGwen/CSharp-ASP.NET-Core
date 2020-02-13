namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public TestController(ITestService testService, ISubjectService subjectService, IMapper mapper)
        {
            this.testService = testService;
            this.subjectService = subjectService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> TestAsync(string id, string teacherId)
        {
            var test = new Test
            {
                Date = DateTime.Now,
                Lesson = await this.subjectService.GetLessonAsync(id),
                LessonId = id,
                TeacherId = teacherId,
            };

            //var model = this.mapper.Map<>()

            return this.View();
        }
    }
}