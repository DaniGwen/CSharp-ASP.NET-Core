using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DigitalCoolBook.App.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TeacherController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel inputModel)
        {
            string returnUrl = "/Home/Index";

            if (ModelState.IsValid)
            {

                var result = await _signInManager
                    .PasswordSignInAsync(inputModel.Email, inputModel.Password, isPersistent: true, lockoutOnFailure: false);

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
            return View();
        }

        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherInputModel InputModel)
        {

            var teacher = new Teacher
            {
                TeacherId = Guid.NewGuid().ToString(),
                DateOfBirth = InputModel.DateOfBirth,
                Email = InputModel.Email,
                MobilePhone = InputModel.MobilePhone,
                Password = this.HashPassword(InputModel.Password),
                PlaceOfBirth = InputModel.PlaceOfBirth,
                Sex = InputModel.Sex,
                Name = InputModel.Name,
                Telephone = InputModel.Telephone
            };

            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = teacher.Email,
                PasswordHash = teacher.Password,
                UserName = teacher.Username,
                NormalizedEmail = teacher.Email.ToUpper(),
                NormalizedUserName = teacher.Email.ToUpper()
            };

            await _context.Users.AddAsync(user);
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return Redirect("/Admin/AdminPanel");
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