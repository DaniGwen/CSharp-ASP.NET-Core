namespace DigitalCoolBook.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.SubjectViewModels;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
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

        [HttpGet]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var categoryLessons = this.subjectService.GetLessons().ToList();
            var subjectsDb = this.subjectService.GetSubjects()
                .Include(s => s.Categories)
                .ToList();

            var subject = subjectsDb.FirstOrDefault(s => s.SubjectId == id);

            var model = this.mapper.Map<SubjectViewModel>(subject);

            return this.View(model);
        }
    }
}