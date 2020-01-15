using DigitalCoolBook.App.Areas.Identity.Pages.Account;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace DigitalCoolBook.App.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class RegisterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterTeacherModel> _logger;
        private readonly ApplicationDbContext _context;

        public RegisterController(
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterTeacherModel> logger,
            ApplicationDbContext context)
        {
            this._context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
      
       

        public IActionResult RegisterStudent()
        {
            return View();
        }
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return hashed.ToString();
        }
    }
}