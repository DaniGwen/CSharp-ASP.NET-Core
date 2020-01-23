using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.App.Services;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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
                    MotherName = registerModel.MotherName,
                };

                var user = new IdentityUser
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                    Id = student.StudentId,

                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                await _userManager.AddToRoleAsync(user, "Student");
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    return View("/Home/SuccessfulySaved");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditStudents()
        {
            var students = _context.Students.ToList();

            var studentsToView = new List<StudentEditViewModel>();

            foreach (var student in students)
            {
                var studentModel = new StudentEditViewModel()
                {
                    StudentId = student.StudentId,
                    Address = student.Address,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    FatherMobileNumber = student.FatherMobileNumber,
                    FatherName = student.FatherName,
                    MobilePhone = student.MobilePhone,
                    MotherMobileNumber = student.MotherMobileNumber,
                    MotherName = student.MotherName,
                    Name = student.Name,
                    PlaceOfBirth = student.PlaceOfBirth,
                    Sex = student.Sex,
                    Telephone = student.Telephone
                };

                studentsToView.Add(studentModel);
            }

            return View(studentsToView);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);

            var model = new StudentEditViewModel
            {
                Address = student.Address,
                Telephone = student.Telephone,
                StudentId = student.StudentId,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                FatherMobileNumber = student.FatherMobileNumber,
                FatherName = student.FatherName,
                MobilePhone = student.MobilePhone,
                MotherMobileNumber = student.MotherMobileNumber,
                MotherName = student.MotherName,
                Name = student.Name,
                PlaceOfBirth = student.PlaceOfBirth,
                Sex = student.Sex
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentEditViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students.FindAsync(id);

                student.Address = model.Address;
                student.DateOfBirth = model.DateOfBirth;
                student.Email = model.Email;
                student.FatherMobileNumber = model.FatherMobileNumber;
                student.FatherName = model.FatherName;
                student.MobilePhone = model.MobilePhone;
                student.MotherMobileNumber = model.MotherMobileNumber;
                student.MotherName = model.MotherName;
                student.Name = model.Name;
                student.PlaceOfBirth = model.PlaceOfBirth;
                student.Sex = model.Sex;
                student.Telephone = model.Telephone;

                await _context.SaveChangesAsync();

                return Redirect("/Home/SuccessfulySaved");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {

            var student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            //Redirect to / Admin / AdminPanel after 4 seconds
            Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");

            return Redirect("/Home/RemoveSuccess");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await _context.Students.FindAsync(id);

            var model = new StudentChangePasswordViewModel()
            {
                Email = student.Email,
                Name = student.Name,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students.FindAsync(id);

                student.Password = this.HashPassword(model.Password);
                await _context.SaveChangesAsync();

                var sendEmail = new EmailSender();
                await sendEmail.SendEmailAsync( "Здравей от digitalcoolbook", "Здрасти!");
                return Redirect("/Home/PasswordSaved");
            }
            return View();
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
