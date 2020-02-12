namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
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

    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUserService userService;
        private readonly IGradeService gradeService;
        private readonly IMapper mapper;
        private UserManager<IdentityUser> userManager;

        public StudentController(
            ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.gradeService = gradeService;
            this.logger = logger;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterStudent()
        {
            var model = new StudentRegisterModel
            {
                Grades = this.gradeService.GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
            };

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterModel registerModel)
        {
            if (this.ModelState.IsValid)
            {
                var student = this.mapper.Map<Student>(registerModel);

                student.UserName = registerModel.Email;

                var result = await this.userManager.CreateAsync(student, registerModel.Password);

                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(student, "Student");

                    this.logger.LogInformation("User created a new account with password.");

                    return this.Redirect("/Home/SuccessfulySaved");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                        return this.View(registerModel);
                    }
                }
            }

            return this.View(registerModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudentsAsync()
        {
            var studentsList = new List<StudentEditViewModel>();

            try
            {
                var students = this.userService.GetStudents().ToList();

                foreach (var student in students)
                {
                    var studentDto = this.mapper.Map<StudentEditViewModel>(student);
                    if (student.GradeId != null)
                    {
                        var grade = await this.gradeService.GetGradeAsync(student.GradeId);
                        studentDto.GradeName = grade.Name;
                    }

                    studentsList.Add(studentDto);
                }
            }
            catch (Exception exception)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = exception.Message,
                };

                return this.View("Error", errorModel);
            }

            return this.View(studentsList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditStudentAsync(string id)
        {
            var student = await this.userService.GetStudentAsync(id);
            var grades = this.gradeService.GetGrades().ToList();

            var model = this.mapper.Map<StudentEditViewModel>(student);

            foreach (var grade in grades)
            {
                var gradeModel = this.mapper.Map<GradeViewModel>(grade);
                model.Grades.Add(gradeModel);
            }

            model.Grades.OrderBy(g => g.Name);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentEditViewModel model, string id)
        {
            if (this.ModelState.IsValid)
            {
                var student = await this.userService.GetStudentAsync(id);

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

                await this.userService.SaveChangesAsync();

                return this.Redirect("/Home/SuccessfulySaved");
            }

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await this.userService.RemoveStudentAsync(id);

            // Redirect to / Admin / AdminPanel after 4 seconds
            this.Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");

            return this.Redirect("/Home/RemoveSuccess");
        }

        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await this.userService.GetStudentAsync(id);

            var model = this.mapper.Map<StudentChangePasswordViewModel>(student);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, Student, Teacher")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model)
        {
            try
            {
                var user = await this.userService.GetUserAsync(model.Id);
                var result = await this.userManager.RemovePasswordAsync(user);
                await this.userService.SaveChangesAsync();
                var addResult = await this.userManager.AddPasswordAsync(user, model.Password);

                if (addResult.Succeeded)
                {
                    await this.signInManager.SignOutAsync();
                    return this.Redirect("/Home/PasswordSaved");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, addResult.Errors.FirstOrDefault().ToString());
                    return this.View(model);
                }
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message,
                    RequestId = this.Request.HttpContext.TraceIdentifier,
                };
                return this.View("Error", error);
            }
        }

        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var userDb = await this.userService.GetUserAsync(userId.Value);
            var model = new StudentChangePasswordViewModel
            {
                Id = userDb.Id,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DetailsAsync(string id)
        {
            var student = await this.userService.GetStudentAsync(id);

            var model = this.mapper.Map<StudentDetailsViewModel>(student);

            return this.View(model);
        }

        public string HashPassword(string password)
        {
            // Add Salt to password
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
