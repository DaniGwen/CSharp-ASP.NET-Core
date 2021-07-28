

using AspNetCoreHero.ToastNotification.Abstractions;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.GradesViewModels;
    using DigitalCoolBook.App.Models.TeacherViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class TeacherController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        private readonly INotyfService _toasterService;
        private readonly UserManager<IdentityUser> _userManager;

        public TeacherController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper,
            INotyfService toasterService)
        {
            _userManager = userManager;
            _userService = userService;
            _gradeService = gradeService;
            _mapper = mapper;
            _toasterService = toasterService;
            _signInManager = signInManager;
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
                var teacher = _mapper.Map<TeacherRegisterModel, Teacher>(registerModel);
                teacher.Id = Guid.NewGuid().ToString();
                teacher.PasswordHash = registerModel.Password;

                if (registerModel.Username == null)
                    teacher.UserName = registerModel.Email;
                else
                    teacher.UserName = registerModel.Username;

                var result = await _userManager.CreateAsync(teacher, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(teacher, "Teacher");

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
            var grades = _gradeService.GetGrades()
                .Include(x => x.Students)
                .Where(grade => grade.GradeTeachers.Count != 0)
                .ToList();

            var gradesViewModel = new List<GradeViewModel>();

            foreach (var grade in grades)
            {
                var gradeDto = _mapper.Map<GradeViewModel>(grade);
                gradeDto.StudentCount = grade.Students.Count;
                gradesViewModel.Add(gradeDto);
            }

            return this.View(gradesViewModel);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> GradeDetailsAsync(string id)
        {
            var studentsInGrade = _userService.GetStudents()
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

            var grade = await _gradeService.GetGradeAsync(id);
            this.ViewData["paraleloName"] = grade.Name;

            return this.View(studentsForView);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.RemoveTeacherAsync(id);
                await _userService.SaveChangesAsync();

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
            var teacher = await this._userService.GetTeacherAsync(id);

            TeacherDetailsViewModel model = this._mapper.Map<TeacherDetailsViewModel>(teacher);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTeacher(TeacherDetailsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var teacher = await _userService.GetTeacherAsync(model.Id);
                this._mapper.Map(model, teacher, typeof(TeacherDetailsViewModel), typeof(Teacher));

                await _userService.SaveChangesAsync();

                this.TempData["SuccessMsg"] = "Changes are saved";

                return this.Redirect("/Home/Success");
            }

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditTeachers()
        {
            var teachers = _userService.GetTeachers().ToList();
            var teacherList = new List<TeacherEditViewModel>();

            foreach (var teacher in teachers)
            {
                var teacherDto = _mapper.Map<TeacherEditViewModel>(teacher);

                teacherList.Add(teacherDto);
            }

            return this.View(teacherList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var teacher = await _userService.GetTeacherAsync(id);

            var model = _mapper.Map<TeacherChangePasswordViewModel>(teacher);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> ChangePassword(TeacherChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userService.GetTeacherAsync(model.Id);
                await _userManager.RemovePasswordAsync(user);
                await _userService.SaveChangesAsync();
                var passwordResult = await _userManager.AddPasswordAsync(user, model.Password);

                if (passwordResult.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    _toasterService.Information("You were signed out");
                    _toasterService.Success("Password saved");

                    return this.Redirect("/Home/Index");
                }

                this.ModelState.AddModelError(string.Empty, passwordResult.Errors
                    .FirstOrDefault()
                    .ToString());
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        [ActionName("Panel")]
        public async Task<IActionResult> PanelAsync()
        {
            var userId = _userManager.GetUserId(this.User);
            var user = await this._userService.GetUserAsync(userId);

            var teacherViewModel = new TeacherChangePasswordViewModel();
            
            if (user != null) { teacherViewModel.Id = user.Id; }

            return this.View(teacherViewModel);
        }
    }
}