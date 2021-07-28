using AspNetCoreHero.ToastNotification.Abstractions;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;
        private readonly INotyfService _toasterService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            INotyfService toasterService)
        {
            _userManager = userManager;
            _userService = userService;
            _toasterService = toasterService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                var user = await _userService.GetUserAsync(userId);

                if (user == null)
                    return this.View();

                if (this.User.IsInRole("Teacher") || this.User.IsInRole("Student"))
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
                        _toasterService.Success("You are logged in");
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

        public void Success()
        {
          _toasterService.Success("Success");
        }

        public void PasswordSaved()
        {
           _toasterService.Success("Password was saved");
        }

        private void AddRefreshHeader() =>
            this.Response.Headers.Add("REFRESH", $"3;URL=/Home/Index");

    }
}
