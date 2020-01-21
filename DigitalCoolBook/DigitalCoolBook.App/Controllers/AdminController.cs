using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.App.Models.TeacherViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Controllers
{
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

        [HttpGet]
        public IActionResult EditTeachers()
        {
            var teachers = _context.Teachers.ToList();
            List<TeacherEditViewModel> teachersForView = new List<TeacherEditViewModel>();

            foreach (var teacher in teachers)
            {
                var teacherForView = new TeacherEditViewModel()
                {
                    TeacherId = teacher.TeacherId,
                    DateOfBirth = teacher.DateOfBirth,
                    Email = teacher.Email,
                    MobilePhone = teacher.MobilePhone,
                    Name = teacher.Name,
                    PlaceOfBirth = teacher.PlaceOfBirth,
                    Sex = teacher.Sex,
                    Telephone = teacher.Telephone,
                    Username = teacher.Username
                };

                teachersForView.Add(teacherForView);
            }

            return View(teachersForView);
        }

        public IActionResult EditStudents()
        {
            var students = _context.Students.ToList();
            var studentsToView = new List<StudentEditViewModel>();

            foreach (var student in students)
            {
                var studentModel = new StudentEditViewModel()
                {
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
    }
}