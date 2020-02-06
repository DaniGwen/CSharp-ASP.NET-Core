namespace DigitalCoolBook.App.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using DigitalCoolBook.App.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;
    using DigitalCoolBook.Models;
    using System;
    using System.Threading.Tasks;
    using DigitalCoolBook.Services.Contracts;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUserService userService;
        private UserManager<IdentityUser> userManager;

        public HomeController(
            ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.logger = logger;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)
                    .Value;
                var user = await this.userService.GetUserAsync(userId);

                if (this.User.IsInRole("Teacher"))
                {
                    var teacher = (Teacher)user;
                    this.ViewData["UserName"] = teacher.Name;
                }

                if (this.User.IsInRole("Student"))
                {
                    var student = (Student)user;
                    this.ViewData["UserName"] = student.Name;
                }

                if (this.User.IsInRole("Admin"))
                {
                    this.ViewData["UserName"] = "Админ";
                }
            }

            return this.View();
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = await this.userManager
                        .FindByEmailAsync(loginModel.Email);
                    var password = await this.userManager
                        .CheckPasswordAsync(user, loginModel.Password);

                    if (password)
                    {
                        var result = await this.signInManager.PasswordSignInAsync(user, loginModel.Password, isPersistent: true, false);
                        this.logger.LogInformation("User logged in.");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, "Грешен имейл или парола.");
                        return this.View(loginModel);
                    }
                }
                catch (Exception exception)
                {
                    var error = new ErrorViewModel
                    {
                        Message = exception.Message,
                        RequestId = this.Request.HttpContext.TraceIdentifier,
                    };

                    this.ModelState.AddModelError(string.Empty, "Грешен имейл или парола.");
                    return this.View("Error", error);
                }
            }

            return this.Redirect("/Home/Index");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SuccessfulySaved()
        {
            this.Response.Headers.Add("REFRESH", "3;URL=/Admin/AdminPanel");
            return this.View();
        }

        public IActionResult PasswordSaved()
        {
            this.Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");
            return this.View();
        }

        [HttpGet]
        public IActionResult RemoveSuccess()
        {
            this.Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");
            return this.View();
        }
    }
}
