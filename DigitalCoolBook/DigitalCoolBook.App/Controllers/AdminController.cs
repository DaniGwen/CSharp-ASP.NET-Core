using DigitalCoolBook.App.Data;
using DigitalCoolBook.App.Models;
using DigitalCoolBook.App.Models.GradeParaleloViewModels;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger)
        {
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdminAsync(LoginAdminViewModel inputModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

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
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();

        }

        public IActionResult AdminContact()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Paralelos()
        {
            var paralelos = _context.GradeParalelos.ToList();
            var models = new List<ParaleloViewModel>();

            foreach (var paralelo in paralelos)
            {
                var model = new ParaleloViewModel
                {
                    Id = paralelo.GradeParaleloId,
                    GradeId = paralelo.IdGrade,
                    GradeName = _context.Grades.FirstOrDefault(g => g.GradeId == paralelo.IdGrade).Name,
                    TeacherName = _context.Teachers.FirstOrDefault(t => t.Id == paralelo.IdTeacher).Name,
                    TeacherId = paralelo.IdTeacher,
                    Students = _context.Students.Where(s => s.GradeId == paralelo.Grade.GradeId).ToList()
                };

                models.Add(model);
            }

            return View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateParalelo()
        {
            var teachers = _context.Teachers
                .ToList();

            var grades = _context.Grades
                .Where(g => g.Students.Any(s => s.GradeId == g.GradeId))
                .ToList();

            var model = new ParaleloCreateViewModel()
            {
                Grades = grades,
                Teachers = teachers
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateParaleloAsync(ParaleloCreateViewModel model)
        {
            try
            {
                    var gradeParalelo = new GradeParalelo()
                    {
                        GradeParaleloId = Guid.NewGuid().ToString(),
                        IdGrade = model.GradeId,
                        IdTeacher = model.TeacherId
                    };

                    await _context.GradeParalelos.AddAsync(gradeParalelo);
                    await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Message = exception.Message
                };
                return View("Error", error);
            }

            return Redirect("/Home/SuccessfulySaved");
        }

        public IActionResult EditParalelo(string id)
        {
            var paralelo = _context.GradeParalelos.Find(id);
            var model = new ParaleloViewModel
            {
                GradeId = paralelo.IdGrade,
                GradeName = paralelo.Grade.Name,
                Id = paralelo.GradeParaleloId,
                TeacherId = paralelo.IdTeacher,
                TeacherName = paralelo.Teacher.Name
            };

            return View(model);
        }
    }
}