namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                var user = await this._userService.GetUserAsync(userId);

                if (user == null)
                    return this.View();

                if (this.User.IsInRole("Teacher"))
                    return Redirect("/Subject/Subjects"); 
                if (this.User.IsInRole("Student"))
                    return Redirect("/Subject/Subjects"); 
                if (this.User.IsInRole("Admin"))
                    return Redirect("/Admin/AdminPanel");
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Login() => this.View();

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager
                        .FindByEmailAsync(loginModel.Email);

                    var isPasswordSuccess = await _userManager
                        .CheckPasswordAsync(user, loginModel.Password);

                    if (isPasswordSuccess)
                    {
                        var result = await _signInManager
                            .PasswordSignInAsync(user, loginModel.Password, isPersistent: true, false);
                        _logger.LogInformation("User logged in.");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, "Wrong email or password!");
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

                    return this.View("Error", error);
                }
            }

            return this.Redirect("/Home/Index");
        }

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });


        [Authorize(Roles = "Teacher, Admin, Student")]
        public IActionResult Success()
        {
            this.AddRefreshHeader();

            return this.View();
        }

        public IActionResult PasswordSaved()
        {
            this.AddRefreshHeader();

            return this.View();
        }

        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult ErrorView()
        {
            this.AddRefreshHeader();

            return this.View();
        }

        private void AddRefreshHeader() =>
            this.Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");

    }
}
