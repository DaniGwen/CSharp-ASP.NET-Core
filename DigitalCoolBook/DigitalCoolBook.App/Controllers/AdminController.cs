using DigitalCoolBook.App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DigitalCoolBook.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditTeacher()
        {
            var teachers = _context.Teachers.ToList();

            return View(teachers);
        }

        public IActionResult LoginAdmin()
        {
            return View();
        }
    }
}