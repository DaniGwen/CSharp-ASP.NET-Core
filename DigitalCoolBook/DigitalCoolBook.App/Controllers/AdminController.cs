﻿using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Controllers
{
    [ValidateAntiForgeryToken]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger)
        {
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdminAsync(LoginAdminViewModel inputModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();

        }

        public IActionResult AdminContact()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateParalelo()
        {

            return View();
        }
    }
}