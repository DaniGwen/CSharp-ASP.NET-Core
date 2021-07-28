using AspNetCoreHero.ToastNotification.Abstractions;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Models;
    using Models.AdminViewModels;
    using Models.GradeParaleloViewModels;
    using Services;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _toasterService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(
            SignInManager<IdentityUser> signInManager,
            IGradeService gradeService,
            IUserService userService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            INotyfService toasterService
            )
        {
            _signInManager = signInManager;
            _gradeService = gradeService;
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _toasterService = toasterService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel() => this.View();

        [HttpGet]
        public IActionResult AdminContact() => this.View();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditParalelos()
        {
            var gradeTeachers = this._gradeService.GetGradeParalelos().ToList();
            var models = new List<ParaleloViewModel>();

            foreach (var gradeTeacher in gradeTeachers)
            {
                var model = new ParaleloViewModel
                {
                    Id = gradeTeacher.GradeTeacherId,
                    GradeId = gradeTeacher.GradeId,
                    TeacherId = gradeTeacher.TeacherId,
                    GradeName = this._gradeService
                    .GetGrades()
                    .FirstOrDefault(g => g.GradeId == gradeTeacher.GradeId)?
                    .Name,
                    TeacherName = this._userService
                    .GetTeachers()
                    .FirstOrDefault(t => t.Id == gradeTeacher.TeacherId)?
                    .Name,
                    Students = this._userService
                    .GetStudents()
                    .Where(s => s.GradeId == gradeTeacher.Grade.GradeId)
                    .ToList(),
                };

                models.Add(model);
            }

            return this.View(models);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditParaleloAsync(string teacherId, string gradeId)
        {
            var gradeParalelo = await _gradeService.GetGradeParaleloAsync(teacherId);

            var grades = _gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList();

            var teachers = _userService
                .GetTeachers()
                .ToList();

            var model = _mapper.Map<ParaleloCreateViewModel>(gradeParalelo);

            model.GradeName = _gradeService
                .GetGrades()
                .FirstOrDefault(g => g.GradeId == gradeParalelo.GradeId)?.Name;

            model.TeacherName = _userService
                .GetTeachers()
                .FirstOrDefault(t => t.Id == gradeParalelo.TeacherId)?.Name;

            model.Teachers = teachers;
            model.Grades = grades;
            model.GradeParaleloId = teacherId;

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateParalelo()
        {
            var teachers = _userService
                .GetTeachers()
                .ToList();

            var grades = _gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList();

            var model = new ParaleloCreateViewModel()
            {
                Grades = grades,
                Teachers = teachers,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateParaleloAsync(ParaleloCreateViewModel model)
        {
            try
            {
                await this._gradeService.AddGradeParaleloAsync(new GradeTeacher
                {
                    GradeTeacherId = Guid.NewGuid().ToString(),
                    GradeId = model.IdGrade,
                    TeacherId = model.IdTeacher
                });
            }
            catch (Exception)
            {
                _toasterService.Error("Error creating a class");
                return this.View();
            }

            _toasterService.Success("Class has been created successfully");
            return Redirect("/Home/Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditParaleloAsync(ParaleloCreateViewModel model)
        {
            try
            {
                var gradeParalelo = await this._gradeService.GetGradeParaleloAsync(model.GradeParaleloId);

                gradeParalelo.TeacherId = model.IdTeacher;
                gradeParalelo.GradeId = model.IdGrade;

                await this._gradeService.SaveChangesAsync();
            }
            catch (Exception)
            {
                _toasterService.Error("Error saving changes");
                return this.View();
            }

            _toasterService.Success("Changes saved successfully");
            return this.Redirect("/Home/Index");
        }

        public async Task<IActionResult> DeleteParaleloAsync(string id)
        {
            try
            {
                var paralelo = await this._gradeService.GetGradeParaleloAsync(id);
                await this._gradeService.RemoveGradeParaleloAsync(paralelo);
                await this._gradeService.SaveChangesAsync();
            }
            catch (Exception)
            {
                _toasterService.Error("Error removing");
                return BadRequest();
            }

            _toasterService.Success("Removed successfully");
            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult ForgotPassword(ForgotPasswordInputMode model)
        {
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(email);
            }

            var user = this._userService.GetUserByEmail(email);

            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, $"{email} no such email registered.");

                return this.View();
            }

            user.PasswordHash = null;

            var newPassword = GeneratePassword(4, 1, 3, 1);

            await this._userManager.AddPasswordAsync(user, newPassword);

            await this._userService.SaveChangesAsync();

            // Sends the new password to the user
            var emailSender = new EmailSender(_configuration);

            await emailSender.SendNewPassword(newPassword, user.Email, user.UserName);

            return this.Json("New password has been send to your email");
        }

        private static string GeneratePassword(int lowercase, int uppercase, int numerics, int symbols)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";
            string specialSymbols = "*=!|@+-_&?'%^.,";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString());
            }

            for (int i = 1; i <= symbols; i++)
            {
                generated = generated.Insert(
                   random.Next(generated.Length),
                   specialSymbols[random.Next(specialSymbols.Length - 1)].ToString());
            }

            for (int i = 1; i <= uppercase; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString());
            }

            for (int i = 1; i <= numerics; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString());
            }

            return generated.Replace("!", string.Empty);
        }
    }
}