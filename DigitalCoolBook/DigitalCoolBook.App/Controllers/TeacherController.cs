using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.GradesViewModels;
using DigitalCoolBook.App.Models.TeacherViewModels;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;

        public TeacherController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
            _gradeService = gradeService;
            _mapper = mapper;
            _logger = logger;
            _signInManager = signInManager;
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
                var teacher = _mapper.Map<TeacherRegisterModel, Teacher>(registerModel);
                teacher.Id = Guid.NewGuid().ToString();
                teacher.PasswordHash = registerModel.Password;

                if (registerModel.Username == null)
                {
                    teacher.UserName = registerModel.Email;
                }
                else
                {
                    teacher.UserName = registerModel.Username;
                }

                var result = await _userManager.CreateAsync(teacher, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(teacher, "Teacher");

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

        public IActionResult ChooseGrade()
        {
            var grades = _gradeService.GetGrades()
                .Where(grade => grade.GradeParalelos.Count != 0)
                .ToList();

            var gradesToView = new List<GradeViewModel>();

            foreach (var grade in grades)
            {
                var gradeDto = _mapper.Map<GradeViewModel>(grade);
                gradesToView.Add(gradeDto);
            }

            return View(gradesToView);
        }

        public IActionResult GradeDetailsAsync(string id)
        {
            var studentsInGrade = _userService.GetStudents()
                .Where(s => s.GradeId == id)
                .Select(s => new
                {
                    s.Id,
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
                    Id = student.Id,
                    Attendances = student.Attendances,
                    ScoreRecords = student.ScoreRecords,
                    Name = student.Name
                };

                studentsForView.Add(model);
            }

            this.ViewData["paraleloName"] = _context.Grades.Find(id).Name;

            return View(studentsForView);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = exception.Message
                };

                return View("Error", errorModel);
            }

            //Redirect to /Admin/AdminPanel after 4 seconds
            //Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");

            return Redirect("/Admin/AdminPanel");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditTeacher(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            TeacherDetailsViewModel model = new TeacherDetailsViewModel
            {
                TeacherId = teacher.Id,
                DateOfBirth = teacher.DateOfBirth,
                Email = teacher.Email,
                MobilePhone = teacher.MobilePhone,
                Name = teacher.Name,
                PlaceOfBirth = teacher.PlaceOfBirth,
                Sex = teacher.Sex,
                Telephone = teacher.Telephone,
                Username = teacher.UserName
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTeacher(TeacherDetailsViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _context.Teachers.FindAsync(id);

                teacher.Name = model.Name;
                teacher.MobilePhone = model.MobilePhone;
                teacher.PlaceOfBirth = model.PlaceOfBirth;
                teacher.Sex = model.Sex;
                teacher.Telephone = model.Telephone;
                teacher.UserName = model.Email;
                teacher.Email = model.Email;

                await _context.SaveChangesAsync();

                return Redirect("/Home/SuccessfulySaved");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditTeachers()
        {
            var teachers = _context.Teachers.ToList();
            List<TeacherEditViewModel> teachersForView = new List<TeacherEditViewModel>();

            foreach (var teacher in teachers)
            {
                var teacherForView = new TeacherEditViewModel()
                {
                    TeacherId = teacher.Id,
                    DateOfBirth = teacher.DateOfBirth,
                    Email = teacher.Email,
                    MobilePhone = teacher.MobilePhone,
                    Name = teacher.Name,
                    PlaceOfBirth = teacher.PlaceOfBirth,
                    Sex = teacher.Sex,
                    Telephone = teacher.Telephone,
                    Username = teacher.UserName
                };

                teachersForView.Add(teacherForView);
            }

            return View(teachersForView);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            var model = new TeacherChangePasswordViewModel()
            {
                Id = id,
                Email = teacher.Email,
                Name = teacher.Name,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(TeacherChangePasswordViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                var result = await _userManager.RemovePasswordAsync(user);
                _context.SaveChanges();
                var addPasswordResult = await _userManager.AddPasswordAsync(user, model.Password);

                if (addPasswordResult.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Home/PasswordSaved");
                }
                else
                {
                    ModelState.AddModelError("", addPasswordResult.Errors.FirstOrDefault().ToString());
                }
                return View(model);
            }
            return View("Error");
        }

        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId.Value);
            var model = new TeacherChangePasswordViewModel
            {
                Id = user.Id,
            };

            return View(model);
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