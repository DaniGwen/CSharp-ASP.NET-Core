namespace DigitalCoolBook.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsController : Controller
    {
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public SubjectsController(ISubjectService subjectService, IMapper mapper)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Subjects()
        {
            var subjects = this.subjectService.GetSubjects().ToList();
            var subjectList = new List<SubjectViewModel>();

            foreach (var subject in subjects)
            {
                var subjectModel = this.mapper.Map<SubjectViewModel>(subject);
                subjectList.Add(subjectModel);
            }

            return this.View(subjectList);
        }
    }
}