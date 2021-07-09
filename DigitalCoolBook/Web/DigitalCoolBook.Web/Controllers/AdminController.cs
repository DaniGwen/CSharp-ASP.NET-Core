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
    using Microsoft.Extensions.Logging;

    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminController(
            SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger,
            IGradeService gradeService,
            IUserService userService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
            )
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.gradeService = gradeService;
            this.userService = userService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
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
            var gradeTeachers = this.gradeService.GetGradeParalelos().ToList();
            var models = new List<ParaleloViewModel>();
        
            foreach (var gradeTeacher in gradeTeachers)
            {
                var model = new ParaleloViewModel
                {
                    Id = gradeTeacher.GradeTeacherId,
                    GradeId = gradeTeacher.GradeId,
                    TeacherId = gradeTeacher.TeacherId,
                    GradeName = this.gradeService
                    .GetGrades()
                    .FirstOrDefault(g => g.GradeId == gradeTeacher.GradeId)?
                    .Name,
                    TeacherName = this.userService
                    .GetTeachers()
                    .FirstOrDefault(t => t.Id == gradeTeacher.TeacherId)?
                    .Name,
                    Students = this.userService
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
            var gradeParalelo = await this.gradeService.GetGradeParaleloAsync(teacherId);

            var grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList();

            var teachers = this.userService
                .GetTeachers()
                .ToList();

            var model = this.mapper.Map<ParaleloCreateViewModel>(gradeParalelo);

            model.GradeName = this.gradeService
                .GetGrades()
                .FirstOrDefault(g => g.GradeId == gradeParalelo.GradeId)?.Name;

            model.TeacherName = this.userService
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
            var teachers = this.userService
                .GetTeachers()
                .ToList();

            var grades = this.gradeService
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
                var gradeParalelo = new GradeTeacher
                {
                    GradeTeacherId = Guid.NewGuid().ToString(),
                    GradeId = model.IdGrade,
                    TeacherId = model.IdTeacher
                };

                await this.gradeService.AddGradeParaleloAsync(gradeParalelo);
            }
            catch (Exception exception)
            {
                return this.View("Error", new ErrorViewModel { Message = exception.Message });
            }

            this.TempData["SuccessMsg"] = "Class has been created successfully";
            return this.Redirect("/Home/Success");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditParaleloAsync(ParaleloCreateViewModel model)
        {
            try
            {
                var gradeParalelo = await this.gradeService.GetGradeParaleloAsync(model.GradeParaleloId);

                gradeParalelo.TeacherId = model.IdTeacher;
                gradeParalelo.GradeId = model.IdGrade;

                await this.gradeService.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message,
                };

                return this.View("Error", error);
            }

            this.TempData["SuccessMsg"] = "Changes saved";
            return this.Redirect("/Home/Success");
        }

        public async Task<IActionResult> DeleteParaleloAsync(string id)
        {
            try
            {
                var paralelo = await this.gradeService.GetGradeParaleloAsync(id);
                await this.gradeService.RemoveGradeParaleloAsync(paralelo);
                await this.gradeService.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message,
                };
                return this.View("Error", error);
            }

            this.TempData["SuccessMsg"] = "Removed successfully";
            return this.Redirect("/Home/Success");
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

            var user = this.userService.GetUserByEmail(email);

            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, $"{email} no such email registered.");

                return this.View();
            }

            user.PasswordHash = null;

            var newPassword = GeneratePassword(4, 1, 3, 1);

            await this.userManager.AddPasswordAsync(user, newPassword);

            await this.userService.SaveChangesAsync();

            // Sends the new password to the user
            var emailSender = new EmailSender(configuration);

            emailSender.SendNewPassword(newPassword, user.Email, user.UserName);

            return this.Json("Нова парола беше изпратена на вашият имейл.");
        }

        // Generates Random password
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