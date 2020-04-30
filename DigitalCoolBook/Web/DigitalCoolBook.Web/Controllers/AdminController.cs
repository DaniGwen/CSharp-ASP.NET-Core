namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.App.Models.AdminViewModels;
    using DigitalCoolBook.App.Models.GradeParaleloViewModels;
    using DigitalCoolBook.App.Services;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly EmailSender emailSender;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminController(
            SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger,
            IGradeService gradeService,
            IUserService userService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            EmailSender emailSender)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.gradeService = gradeService;
            this.userService = userService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return this.View();
        }

        public IActionResult LoginAdmin()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdminAsync(LoginAdminViewModel inputModel, string returnUrl)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    return this.LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return this.RedirectToPage("./Lockout");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(inputModel);
                }
            }

            return this.View();
        }

        public IActionResult AdminContact()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditParalelos()
        {
            var paralelos = this.gradeService.GetGradeParalelos().ToList();
            var models = new List<ParaleloViewModel>();

            foreach (var paralelo in paralelos)
            {
                var grades = this.gradeService
                    .GetGrades()
                    .ToList();

                var model = new ParaleloViewModel
                {
                    Id = paralelo.GradeTeacherId,

                    GradeId = paralelo.GradeId,

                    TeacherId = paralelo.TeacherId,

                    GradeName = this.gradeService
                    .GetGrades()
                    .FirstOrDefault(g => g.GradeId == paralelo.GradeId)
                    .Name,

                    TeacherName = this.userService
                    .GetTeachers()
                    .FirstOrDefault(t => t.Id == paralelo.TeacherId)
                    .Name,

                    Students = this.userService
                    .GetStudents()
                    .Where(s => s.GradeId == paralelo.Grade.GradeId)
                    .ToList(),
                };

                models.Add(model);
            }

            return this.View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateParaleloAsync(ParaleloCreateViewModel model)
        {
            try
            {
                var gradeParalelo = this.mapper.Map<GradeTeacher>(model);

                gradeParalelo.GradeTeacherId = Guid.NewGuid().ToString();

                await this.gradeService.AddGradeParaleloAsync(gradeParalelo);
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message,
                };
                return this.View("Error", error);
            }

            this.TempData["SuccessMsg"] = "Паралелката създадена успешно";
            return this.Redirect("/Home/Success");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditParaleloAsync(string id)
        {
            var paralelo = await this.gradeService.GetGradeParaleloAsync(id);

            var grades = this.gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList();

            var teachers = this.userService
                .GetTeachers()
                .ToList();

            var model = this.mapper.Map<ParaleloCreateViewModel>(paralelo);

            model.GradeName = this.gradeService
                .GetGrades()
                .FirstOrDefault(g => g.GradeId == paralelo.GradeId).Name;

            model.TeacherName = this.userService
                .GetTeachers()
                .FirstOrDefault(t => t.Id == paralelo.TeacherId).Name;

            model.Teachers = teachers;
            model.Grades = grades;

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
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

            this.TempData["SuccessMsg"] = "Промяната е записана успешно";
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

            this.TempData["SuccessMsg"] = "Премахването е успешно";
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
                this.ModelState.AddModelError(string.Empty, $"{email} няма регистриран такъв имейл.");

                return this.View();
            }

            user.PasswordHash = null;

            var newPassword = GeneratePassword(4, 1, 3, 1);

            await this.userManager.AddPasswordAsync(user, newPassword);

            await this.userService.SaveChangesAsync();

            var result = this.emailSender.SendNewPassword(newPassword, user.Email);

            return this.Json(result);
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