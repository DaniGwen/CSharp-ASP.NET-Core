using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.TeacherViewModels;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalCoolBook.App.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherController(ILogger<HomeController> logger,
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

                var teacher = _context.Teachers
                    .FirstOrDefault(t => t.Password == this.HashPassword(loginModel.Password));

                if (teacher != null)
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
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher
                {
                    TeacherId = Guid.NewGuid().ToString(),
                    DateOfBirth = registerModel.DateOfBirth,
                    Email = registerModel.Email,
                    MobilePhone = registerModel.MobilePhone,
                    Password = this.HashPassword(registerModel.Password),
                    PlaceOfBirth = registerModel.PlaceOfBirth,
                    Sex = registerModel.Sex,
                    Name = registerModel.Name,
                    Telephone = registerModel.Telephone
                };

                var user = new IdentityUser
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                await _userManager.AddToRoleAsync(user, "Teacher");
                //await _context.Users.AddAsync(user);
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return Redirect("/Home/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return Redirect("/Admin/AdminPanel");
        }

        public IActionResult ChooseParalelo(GradesViewModel model)
        {
            var grades = _context.Grades.Where(grade => grade.GradeParalelos.Count != 0).ToList();
            var gradesForView = new List<GradesViewModel>();

            foreach (var grade in grades)
            {
                var gradeForList = new GradesViewModel
                {
                    Id = grade.GradeId,
                    Name = grade.Name
                };
                gradesForView.Add(gradeForList);
            }
            return View(gradesForView);
        }

        public IActionResult GradeDetails(string id)
        {
            var studentsInGrade = _context.Students
                .Include(s => s.GradeParalelo)
                .Where(s => s.IdGradeParalelo == id)
                .Select(s => new
                {
                    s.Name,
                    s.ScoreRecords,
                    s.Attendances
                })
                .ToList();

            var studentsForView = new List<GradeDetailViewModel>();

            foreach (var student in studentsInGrade)
            {
                GradeDetailViewModel model = new GradeDetailViewModel
                {
                    Attendances = student.Attendances,
                    ScoreRecords = student.ScoreRecords,
                    Name = student.Name
                };

                studentsForView.Add(model);
            }

            return View(studentsForView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                return View("Error", exception);
            }

            //Redirect to /Admin/AdminPanel after 4 seconds
            Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");
            return Redirect("/Home/RemoveTeacherSuccess");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            TeacherDetailsViewModel model = new TeacherDetailsViewModel
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

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(TeacherDetailsViewModel model, string id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);

                teacher.Name = model.Name;
                teacher.MobilePhone = model.MobilePhone;
                teacher.PlaceOfBirth = model.PlaceOfBirth;
                teacher.Sex = model.Sex;
                teacher.Telephone = model.Telephone;
                teacher.Username = model.Username;
                teacher.Email = model.Email;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return Redirect("/Home/SuccessfulySaved");
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