using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DigitalCoolBook.App.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Subjects()
        {
            var subjects = _context.Subjects.ToList();
            var listOfSubjects = new List<SubjectViewModel>();

            foreach (var subject in subjects)
            {
                var subjectModel = new SubjectViewModel
                {
                    SubjectId = subject.SubjectId,
                    Name = subject.Name
                };

                listOfSubjects.Add(subjectModel);
            }

            return View(listOfSubjects);
        }
    }
}