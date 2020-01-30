using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DigitalCoolBook.App.Models;
using Microsoft.AspNetCore.Identity;
using DigitalCoolBook.App.Data;
using System.Security.Claims;
using DigitalCoolBook.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);
                    var password = await _userManager.CheckPasswordAsync(user, loginModel.Password);

                    if (password)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, isPersistent: true, false);
                        _logger.LogInformation("User logged in.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Грешен имейл или парола.");
                        return View(loginModel);
                    }
                }
                catch (Exception exception)
                {
                    var error = new ErrorViewModel
                    {
                        Message = exception.Message,
                        RequestId = Request.HttpContext.TraceIdentifier
                    };

                    ModelState.AddModelError(string.Empty, "Грешен имейл или парола.");
                    return View("Error", error);
                }
            }
            return Redirect("/Home/Index");
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
