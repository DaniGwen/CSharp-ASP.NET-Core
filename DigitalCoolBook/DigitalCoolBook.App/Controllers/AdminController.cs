namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.App.Models.GradeParaleloViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminController(
            SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger,
            IGradeService gradeService,
            IUserService userService,
            IMapper mapper)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.gradeService = gradeService;
            this.userService = userService;
            this.mapper = mapper;
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
                    Id = paralelo.GradeParaleloId,

                    GradeId = paralelo.IdGrade,

                    TeacherId = paralelo.IdTeacher,

                    GradeName = this.gradeService
                    .GetGrades()
                    .FirstOrDefault(g => g.GradeId == paralelo.IdGrade)
                    .Name,

                    TeacherName = this.userService
                    .GetTeachers()
                    .FirstOrDefault(t => t.Id == paralelo.IdTeacher)
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
                var gradeParalelo = this.mapper.Map<GradeParalelo>(model);

                gradeParalelo.GradeParaleloId = Guid.NewGuid().ToString();

                await this.gradeService.AddGradeParaleloAsync(gradeParalelo);
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

            return this.Redirect("/Home/SuccessfulySaved");
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
                .FirstOrDefault(g => g.GradeId == paralelo.IdGrade).Name;

            model.TeacherName = this.userService
                .GetTeachers()
                .FirstOrDefault(t => t.Id == paralelo.IdTeacher).Name;

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

                gradeParalelo.IdTeacher = model.IdTeacher;
                gradeParalelo.IdGrade = model.IdGrade;

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

            return this.Redirect("/Home/SuccessfulySaved");
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

            return this.Redirect("/Home/RemoveSuccess");
        }
    }
}