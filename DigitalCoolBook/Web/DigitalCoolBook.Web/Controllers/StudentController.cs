namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.GradesViewModels;
    using DigitalCoolBook.App.Models.StudentViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using AspNetCoreHero.ToastNotification.Abstractions;

    public class StudentController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        private readonly IScoreService _scoreService;
        private readonly ITestService _testService;
        private readonly INotyfService _toasterService;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserService userService,
            IGradeService gradeService,
            IMapper mapper,
            IScoreService scoreService,
            ITestService testService,
            INotyfService toasterService)
        {
            _userManager = userManager;
            _userService = userService;
            _gradeService = gradeService;
            _signInManager = signInManager;
            _mapper = mapper;
            _scoreService = scoreService;
            _testService = testService;
            _toasterService = toasterService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterStudent()
        {
            var model = new StudentRegisterInputModel
            {
                Grades = _gradeService.GetGrades()
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
                var student = _mapper.Map<Student>(registerModel);

                student.UserName = registerModel.Email;

                var result = await _userManager.CreateAsync(student, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(student, "Student");

                    _toasterService.Success("Account registered");
                    return this.Redirect("/Home/Index");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View(registerModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudentsAsync()
        {
            var studentsList = new List<StudentEditViewModel>();

            try
            {
                var students = _userService.GetStudents().ToList();

                foreach (var student in students)
                {
                    var studentDto = _mapper.Map<StudentEditViewModel>(student);
                    if (student.GradeId != null)
                    {
                        var grade = await _gradeService.GetGradeAsync(student.GradeId);
                        studentDto.GradeName = grade.Name;
                    }

                    studentsList.Add(studentDto);
                }
            }
            catch (Exception)
            {
                _toasterService.Error("Error occurred");
                return this.Redirect("/Home/Index");
            }

            return this.View(studentsList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudentAsync(string id)
        {
            var student = await _userService.GetStudentAsync(id);
            var grades = _gradeService.GetGrades()
                .OrderBy(grade => grade.Name)
                .ToList();

            var studentViewModel = _mapper.Map<StudentEditViewModel>(student);
            var gradeModel = _mapper.Map<List<GradeViewModel>>(grades);

            studentViewModel.Grades.AddRange(gradeModel);
            studentViewModel.DateOfBirth = student.DateOfBirth.Date;

            return this.View(studentViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStudent(StudentEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var student = await _userService.GetStudentAsync(model.Id);

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

                _toasterService.Success("Changes were saved");

                return this.Redirect("/Home/Index");
            }

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await _userService.RemoveStudentAsync(id);

            _toasterService.Success("Account has been removed");
            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Student, Teacher")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var student = await _userService.GetStudentAsync(id);

            var model = _mapper.Map<StudentChangePasswordViewModel>(student);

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
                    var user = await _userService.GetUserAsync(model.Id);
                    await _userManager.RemovePasswordAsync(user);
                    await _userService.SaveChangesAsync();
                    var passwordResult = await _userManager.AddPasswordAsync(user, model.Password);

                    if (passwordResult.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        _toasterService.Success("Password saved");
                        _toasterService.Information("You were signed out");

                        return this.Redirect("/Home/Index");
                    }

                    this.ModelState.AddModelError(string.Empty, passwordResult.Errors.FirstOrDefault().ToString());
                }

                return this.View(model);
            }
            catch (Exception)
            {
                _toasterService.Error("Error occurred while changing the password");

                return this.Redirect("/Home/Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public async Task<IActionResult> Panel()
        {
            var userId = _userManager.GetUserId(User);
            var userDb = await this._userService.GetUserAsync(userId);

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
            var student = await this._userService.GetStudentAsync(id);

            var studentDetailsViewModel = this._mapper.Map<StudentDetailsViewModel>(student);

            studentDetailsViewModel.AverageScore = CalcAverageScore(id);
            studentDetailsViewModel.TestsTaken.AddRange(this._testService
                 .GetExpiredTests()
                 .Where(t => t.StudentId == id)
                 .Select(x => x.TestName)
                 .ToList());

            return this.View(studentDetailsViewModel);
        }

        [Authorize(Roles = "Student")]
        public IActionResult GetAverageScore(string id)
        {
            double averageScore = CalcAverageScore(id);

            return this.Json(averageScore);
        }

        private double CalcAverageScore(string studentId)
        {
            var studentScores = this._scoreService.GetScoreStudents()
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
