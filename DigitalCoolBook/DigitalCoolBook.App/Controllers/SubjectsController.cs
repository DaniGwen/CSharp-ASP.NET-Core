using DigitalCoolBook.App.Data;
using Microsoft.AspNetCore.Mvc;
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

            return View(subjects);
        }
    }
}