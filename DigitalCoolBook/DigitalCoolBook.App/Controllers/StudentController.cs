using AutoMapper;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.GradesViewModels;
using DigitalCoolBook.App.Models.StudentViewModels;
using DigitalCoolBook.Models;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;

        public StudentController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _gradeService = gradeService;
            _logger = logger;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterStudent()
        {
            var model = new StudentRegisterModel
            {
                Grades = _gradeService.GetGrades().ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterModel registerModel)
        {
            //var student = new Student();

            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(registerModel);

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
            return View(registerModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudentsAsync()
        {
            var studentsList = new List<StudentEditViewModel>();

            try
            {
                var students = _userService.GetStudents();

                foreach (var student in students)
                {
                    var studentDto = _mapper.Map<StudentEditViewModel>(student);
                    var grade = await _gradeService.GetGradeAsync(student.GradeId);
                    studentDto.GradeName = grade.Name;
                    studentsList.Add(studentDto);
                }
            }
            catch (Exception exception)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = exception.Message
                };

                return View("Error", errorModel);
            }

            return View(studentsList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditStudentAsync(string id)
        {
            var student = await _userService.GetStudentAsync(id);
            var grades = _gradeService.GetGrades().ToList();

            var model = _mapper.Map<StudentEditViewModel>(student);

            foreach (var grade in grades)
            {
                var gradeModel = _mapper.Map<GradeViewModel>(grade);
                model.Grades.Add(gradeModel);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentEditViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var student = await _userService.GetStudentAsync(id);

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
                student.GradeId = model.GradeId;

                await _userService.SaveChangesAsync();

                return Redirect("/Home/SuccessfulySaved");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await _userService.RemoveStudentAsync(id);

            //Redirect to / Admin / AdminPanel after 4 seconds
            Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");

            return Redirect("/Home/RemoveSuccess");
        }


        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await _userService.GetStudentAsync(id);

            var model = _mapper.Map<StudentChangePasswordViewModel>(student);

            return View(model);
        }

        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model)
        {
            try
            {
                var user = await _userService.GetUserAsync(model.Id);
                var result = await _userManager.RemovePasswordAsync(user);
                await _userService.SaveChangesAsync();
                var addResult = await _userManager.AddPasswordAsync(user, model.Password);

                if (addResult.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Home/PasswordSaved");
                }
                else
                {
                    ModelState.AddModelError("", addResult.Errors.FirstOrDefault().ToString());
                    return View(model);
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
        }

        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var userDb = await _userService.GetUserAsync(userId.Value);
            var model = new StudentChangePasswordViewModel
            {
                Id = userDb.Id,
            };

            return View(model);
        }

        public async Task<IActionResult> DetailsAsync(string id)
        {
            var student = await _userService.GetStudentAsync(id);

            var model = _mapper.Map<StudentDetailsViewModel>(student);

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
