using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StudentController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginModel)
        {
            string returnUrl = "/Home/Index";

            if (ModelState.IsValid)
            {
                var hash = this.HashPassword(loginModel.Password);

                var student = _context.Students
                    .FirstOrDefault(t => t.Password == this.HashPassword(loginModel.Password));

                if (student != null)
                {
                    var user = new IdentityUser { UserName = loginModel.Email, Email = loginModel.Email };

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

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
                        return View(loginModel);
                    }
                }
            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    StudentId = Guid.NewGuid().ToString(),
                     Address = registerModel.Address,
                    Email = registerModel.Email,
                    MobilePhone = registerModel.MobilePhone,
                    Password = this.HashPassword(registerModel.Password),
                    PlaceOfBirth = registerModel.PlaceOfBirth,
                    Sex = registerModel.Sex,
                    Name = registerModel.Name,
                    Telephone = registerModel.Telephone,
                    DateOfBirth = registerModel.DateOfBirth,
                    FatherMobileNumber = registerModel.FatherMobileNumber,
                    FatherName = registerModel.FatherName,
                    MotherMobileNumber = registerModel.MotherMobileNumber,
                    MotherName  = registerModel.MotherName,
                };

                var user = new IdentityUser
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Student");
                    _logger.LogInformation("User created a new account with password.");
                   
                    return View("/Home/StudentCreated", student);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Redirect("/Admin/AdminPanel");
        }

        public string HashPassword(string password)
        {
            //Add Salt to password
            byte[] salt = new byte[128 / 8];


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