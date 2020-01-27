using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;

        public StudentController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
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
            try
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                var password = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                if (result.Succeeded)
                {
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

                return View("Error", error);
            }

            return Redirect("/Home/Index");
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
            var student = new Student();

            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid().ToString();
                student.DateOfBirth = registerModel.DateOfBirth;
                student.Email = registerModel.Email;
                student.MobilePhone = registerModel.MobilePhone;
                student.PasswordHash = registerModel.Password;
                student.PlaceOfBirth = registerModel.PlaceOfBirth;
                student.Sex = registerModel.Sex;
                student.Name = registerModel.Name;
                student.Telephone = registerModel.Telephone;
                student.UserName = registerModel.Email;

                var result = await _userManager.CreateAsync(student, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(student, "Student");

                    _logger.LogInformation("User created a new account with password.");

                    return Redirect("/Home/SuccessfulySaved");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(registerModel);
                    }
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
                    StudentId = student.Id,
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
                StudentId = student.Id,
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
            _context.SaveChanges();

            //Redirect to / Admin / AdminPanel after 4 seconds
            Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");

            return Redirect("/Home/RemoveSuccess");
        }


        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await _context.Students.FindAsync(id);

            var model = new StudentChangePasswordViewModel()
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model)
        {
            try
            {
                var user = await _context.Users.FindAsync(model.Id);
                var result = await _userManager.RemovePasswordAsync(user);
                _context.SaveChanges();
                var addResult = await _userManager.AddPasswordAsync(user, model.Password);

                if (addResult.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Home/PasswordSaved");
                }
                else
                {
                    ModelState.AddModelError("", addResult.Errors.FirstOrDefault().ToString());
                }

            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message,
                    RequestId = this.Request.HttpContext.TraceIdentifier
                };
                return View("Error", error);
            }

            return View(model);
        }

        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var userDb = await _context.Users.FindAsync(userId.Value);
            var model = new StudentChangePasswordViewModel
            {
                Id = userDb.Id,
            };

            return View( model);
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
