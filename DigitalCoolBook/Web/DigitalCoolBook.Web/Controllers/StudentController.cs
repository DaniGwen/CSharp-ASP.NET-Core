﻿using DigitalCoolBook.App.Models.TestviewModels;

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
        private readonly IScoreService scoreService;
        private readonly ITestService testService;
        private readonly UserManager<IdentityUser> userManager;

        public StudentController(
            ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper,
            IScoreService scoreService,
            ITestService testService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.gradeService = gradeService;
            this.logger = logger;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.scoreService = scoreService;
            this.testService = testService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterStudent()
        {
            var model = new StudentRegisterInputModel
            {
                Grades = this.gradeService.GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterStudent(StudentRegisterInputModel registerModel)
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

                    this.TempData["SuccessMsg"] = "Account register is successful";
                    return this.Redirect("/Home/Success");
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudentAsync(string id)
        {
            var student = await this.userService.GetStudentAsync(id);
            var grades = this.gradeService.GetGrades().OrderBy(grade => grade.Name).ToList();

            var model = this.mapper.Map<StudentEditViewModel>(student);

            var gradeModel = this.mapper.Map<List<GradeViewModel>>(grades);
            model.Grades.AddRange(gradeModel);
            model.DateOfBirth = student.DateOfBirth.Date;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudent(StudentEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var student = await this.userService.GetStudentAsync(model.Id);

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
                this.TempData["SuccessMsg"] = "Changes saved successfully";

                return this.Redirect("/Home/Success");
            }

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await this.userService.RemoveStudentAsync(id);

            this.Response.Headers.Add("REFRESH", "4;URL=/Admin/AdminPanel");
            this.TempData["SuccessMsg"] = "Account has been removed";
            return this.Redirect("/Home/Success");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Student, Teacher")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await this.userService.GetStudentAsync(id);

            var model = this.mapper.Map<StudentChangePasswordViewModel>(student);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Student, Teacher")]
        public async Task<IActionResult> ChangePassword(StudentChangePasswordViewModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var user = await this.userService.GetUserAsync(model.Id);
                    var result = await this.userManager.RemovePasswordAsync(user);
                    await this.userService.SaveChangesAsync();
                    var addResult = await this.userManager.AddPasswordAsync(user, model.Password);

                    if (addResult.Succeeded)
                    {
                        await this.signInManager.SignOutAsync();
                        this.TempData["SuccessMsg"] = "Паролата е записана успешно";
                        return this.Redirect("/Home/Success");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, addResult.Errors.FirstOrDefault().ToString());
                        return this.View(model);
                    }
                }
                else
                {
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

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public async Task<IActionResult> Panel()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var userDb = await this.userService.GetUserAsync(userId?.Value);

            var model = new StudentChangePasswordViewModel
            {
                Id = userDb.Id,
            };

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var student = await this.userService.GetStudentAsync(id);

            var viewModel = this.mapper.Map<StudentDetailsViewModel>(student);
            viewModel.AverageScore = CalcAverageScore(id);
            viewModel.TestsTaken.AddRange(this.testService
                 .GetExpiredTests()
                 .Where(t => t.StudentId == id)
                 .Select(x => x.TestName)
                 .ToList());

            return this.View(viewModel);
        }

        [Authorize(Roles = "Student")]
        public IActionResult GetAverageScore(string id)
        {
            double averageScore = CalcAverageScore(id);

            return this.Json(averageScore);
        }

        private double CalcAverageScore(string studentId)
        {
            var studentScores = this.scoreService.GetScoreStudents()
                .Where(s => s.StudentId == studentId)
                .ToList();

            double averageScore = 0;

            if (studentScores.Count > 0)
            {
                averageScore = studentScores.Average(s => s.Score.ScorePoints);
            }

            return averageScore;
        }
    }
}
