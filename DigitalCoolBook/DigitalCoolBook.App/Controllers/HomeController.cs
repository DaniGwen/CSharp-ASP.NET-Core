using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DigitalCoolBook.App.Models;
using Microsoft.AspNetCore.Identity;
using DigitalCoolBook.App.Data;
using System.Security.Claims;
using DigitalCoolBook.Models;
using System.Linq;

namespace DigitalCoolBook.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                if (User.IsInRole("Teacher"))
                {
                    var teacher = (Teacher)user;
                    ViewData["UserName"] = teacher.Name;
                }
                if (User.IsInRole("Student"))
                {
                    var student = (Student)user;
                    ViewData["UserName"] = student.Name;
                }
                if (User.IsInRole("Admin"))
                {
                    ViewData["UserName"] = "Админ";
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SuccessfulySaved()
        {
            Response.Headers.Add("REFRESH", "3;URL=/Admin/AdminPanel");
            return View();
        }

        public IActionResult PasswordSaved()
        {
            Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");
            return View();
        }

        [HttpGet]
        public IActionResult RemoveSuccess()
        {
            this.Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");

            return View();
        }
    }
}
