using Microsoft.EntityFrameworkCore;

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
    using DigitalCoolBook.App.Models.TeacherViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class TeacherController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUserService userService;
        private readonly IGradeService gradeService;
        private readonly IMapper mapper;
        private UserManager<IdentityUser> userManager;

        public TeacherController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.gradeService = gradeService;
            this.mapper = mapper;
            this.logger = logger;
            this.signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterModel registerModel)
        {
            if (this.ModelState.IsValid)
            {
                var teacher = this.mapper.Map<TeacherRegisterModel, Teacher>(registerModel);
                teacher.Id = Guid.NewGuid().ToString();
                teacher.PasswordHash = registerModel.Password;

                if (registerModel.Username == null)
                    teacher.UserName = registerModel.Email;
                else
                    teacher.UserName = registerModel.Username;

                var result = await this.userManager.CreateAsync(teacher, registerModel.Password);

                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(teacher, "Teacher");

                    this.TempData["SuccessMsg"] = "The account has been created";
                    return this.Redirect("/Home/Success");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                    return this.View(registerModel);
                }
            }

            return this.View();
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult AssignedGrades()
        {
            var grades = this.gradeService.GetGrades()
                .Include(x=>x.Students)
                .Where(grade => grade.GradeTeachers.Count != 0)
                .ToList();

            var gradesViewModel = new List<GradeViewModel>();

            foreach (var grade in grades)
            {
                var gradeDto = this.mapper.Map<GradeViewModel>(grade);
                gradeDto.StudentCount = grade.Students.Count;
                gradesViewModel.Add(gradeDto);
            }

            return this.View(gradesViewModel);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> GradeDetailsAsync(string id)
        {
            var studentsInGrade = this.userService.GetStudents()
                .Where(s => s.GradeId == id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.ScoreRecords,
                    s.Attendances,
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
                    Name = student.Name,
                };

                studentsForView.Add(model);
            }

            var grade = await this.gradeService.GetGradeAsync(id);
            this.ViewData["paraleloName"] = grade.Name;

            return this.View(studentsForView);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await this.userService.RemoveTeacherAsync(id);
                await this.userService.SaveChangesAsync();

                return this.Json("Account has been deleted");
            }
            catch (Exception)
            {
                return this.Json("Error deleting account");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditTeacher(string id)
        {
            var teacher = await this.userService.GetTeacherAsync(id);

            TeacherDetailsViewModel model = this.mapper.Map<TeacherDetailsViewModel>(teacher);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTeacher(TeacherDetailsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var teacher = await this.userService.GetTeacherAsync(model.Id);
                this.mapper.Map(model, teacher, typeof(TeacherDetailsViewModel), typeof(Teacher));

                await this.userService.SaveChangesAsync();

                this.TempData["SuccessMsg"] = "Changes are saved";

                return this.Redirect("/Home/Success");
            }

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditTeachers()
        {
            var teachers = this.userService.GetTeachers().ToList();
            var teacherList = new List<TeacherEditViewModel>();

            foreach (var teacher in teachers)
            {
                var teacherDto = this.mapper.Map<TeacherEditViewModel>(teacher);

                teacherList.Add(teacherDto);
            }

            return this.View(teacherList);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var teacher = await this.userService.GetTeacherAsync(id);

            var model = this.mapper.Map<TeacherChangePasswordViewModel>(teacher);

            return this.View(model);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(TeacherChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userService.GetTeacherAsync(model.Id);
                await this.userManager.RemovePasswordAsync(user);
                await this.userService.SaveChangesAsync();
                var addPasswordResult = await this.userManager.AddPasswordAsync(user, model.Password);

                if (addPasswordResult.Succeeded)
                {
                    await this.signInManager.SignOutAsync();
                    this.TempData["SuccessMsg"] = "Password saved";
                    return this.Redirect("/Home/Success");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, addPasswordResult.Errors
                        .FirstOrDefault()
                        .ToString());
                }

                return this.View(model);
            }

            return this.View("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId?.Value);
            var model = new TeacherChangePasswordViewModel();

            if (user != null)
            {
                model.Id = user.Id;
            }

            return this.View(model);
        }
    }
}