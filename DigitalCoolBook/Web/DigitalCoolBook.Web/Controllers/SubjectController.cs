namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Models.CategoryViewModels;
    using Models.SubjectViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DigitalCoolBook.Web.Models.CategoryViewModels;
    using DigitalCoolBook.Web.Models.SubjectViewModels;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;

    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IScoreService _scoreService;
        private readonly INotyfService _toasterService;
        private readonly UserManager<IdentityUser> _userManager;

        public SubjectController(
            ISubjectService subjectService,
            IMapper mapper,
            IScoreService scoreService,
            INotyfService toasterService,
            UserManager<IdentityUser> userManager)
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _scoreService = scoreService;
            _toasterService = toasterService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public IActionResult Subjects()
        {
            var subjects = _subjectService.GetSubjects().ToList();
            var subjectList = new List<SubjectViewModel>();

            foreach (var subject in subjects)
            {
                var subjectModel = _mapper.Map<SubjectViewModel>(subject);
                subjectList.Add(subjectModel);
            }

            return this.View(subjectList);
        }

        [HttpGet]
        [ActionName("Categories")]
        public IActionResult CategoriesAsync(string subjectId, string categoryId)
        {
            var subjectDb = _subjectService.GetSubjects()
                .Include(s => s.Categories)
                .FirstOrDefault(s => s.SubjectId == subjectId);

            var model = _mapper.Map<SubjectViewModel>(subjectDb);

            return this.View(model);
        }

        [HttpGet]
        [ActionName("CategoryDetails")]
        [Authorize(Roles = "Admin, Teacher, Student")]
        public IActionResult CategoryDetailsAsync(CategoryDetailsViewModel categoryDetailsModel)
        {
            var lessons = _subjectService.GetLessons()
                .Where(lesson => lesson.CategoryId == categoryDetailsModel.CategoryId)
                .ToList();

            var lessonsDto = _mapper.Map<List<LessonsViewModel>>(lessons);

            // If User is Student adding score in view
            if (this.User.IsInRole("Student"))
            {
                foreach (var lesson in lessonsDto)
                {
                    var studentId = _userManager.GetUserId(this.User);

                    var score = _scoreService
                        .GetScoreStudents()
                        .Where(ss => ss.StudentId == studentId)
                        .FirstOrDefault(s => s.Score.LessonId == lesson.LessonId);

                    // Set Score if there is any
                    lesson.Score = score?.Score.ScorePoints ?? 0;
                }
            }

            return this.View(new CategoryDetailsViewModel
            {
                CategoryId = categoryDetailsModel.CategoryId,
                CategoryTitle = categoryDetailsModel.CategoryTitle,
                Lessons = lessonsDto,
                SubjectId = categoryDetailsModel.SubjectId,
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddLesson()
        {
            var subjects = _subjectService.GetSubjects().ToList();

            var model = new CategoryCreateViewModel
            {
                Categories = new List<Category>(),
                Subjects = subjects,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("AddLesson")]
        public async Task<IActionResult> AddLessonAsync(string title, string content, string categoryId, string level)
        {
            try
            {
                var unlockLevel = int.Parse(level);

                var lesson = new Lesson
                {
                    LessonId = Guid.NewGuid().ToString(),
                    CategoryId = categoryId,
                    Content = content,
                    Title = title,
                    Level = unlockLevel,
                };

                if (unlockLevel == 1) { lesson.IsUnlocked = true; }

                await _subjectService.CreateLessonAsync(lesson);

                return this.Json("The topic has been added");
            }
            catch (Exception)
            {
                return this.Json("Error saving the topic");
            }
        }

        [HttpPost]
        public JsonResult GetCategories(string subjectId)
        {
            var categories = _subjectService
                .GetCategories()
                .Where(c => c.SubjectId == subjectId)
                .ToList();

            var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryAjaxViewModel>>(categories);

            return this.Json(categoriesDto);
        }

        [HttpPost]
        public JsonResult GetLessons(string categoryId)
        {
            var lessons = this._subjectService.GetLessons()
                .Where(l => l.CategoryId == categoryId);

            var model = this._mapper.Map<List<LessonsViewModel>>(lessons);

            return this.Json(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("EditLesson")]
        public async Task<IActionResult> EditLessonAsync(string id)
        {
            var lesson = await _subjectService.GetLessonAsync(id);

            var lessonViewModel = _mapper.Map<LessonEditViewModel>(lesson);

            var categories = _subjectService.GetCategories().ToList();

            var categoriesDto = this._mapper.Map<IList<CategoryViewModel>>(categories);

            lessonViewModel.Categories = categoriesDto;

            return this.View(lessonViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("EditLesson")]
        public async Task<IActionResult> EditLessonAsync(EditLessonPostViewModel viewModel)
        {
            try
            {
                var lesson = await _subjectService.GetLessonAsync(viewModel.LessonId);

                lesson.Title = viewModel.Title;
                lesson.Content = viewModel.Content;
                lesson.CategoryId = viewModel.CategoryId;

                await _subjectService.SaveChangesAsync();

                return this.Json("Changes were saved");
            }
            catch (Exception)
            {
                return this.Json("Error occurred while saving");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateSubject()
        {
            return this.View(new SubjectCreateViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSubjectAsync(SubjectCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (_subjectService.GetSubjects().Any(s => s.Name == model.Name))
                {
                    this.ModelState.AddModelError(string.Empty, "The subject already exist");

                    return this.View(model);
                }

                await _subjectService.CreateSubjectAsync(new Subject
                {
                    Name = model.Name,
                    SubjectId = Guid.NewGuid().ToString(),
                });
            }
            else
            {
                return this.View(model);
            }

            _toasterService.Success("Subject has been created");

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCategory()
        {
            var subjects = this._subjectService.GetSubjects().ToList();

            var model = new CategoryAdminCreateViewModel()
            {
                Subjects = _mapper.Map<List<SubjectViewModel>>(subjects),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync(CategoryAdminCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _subjectService.CreateCategoryAsync(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    SubjectId = model.SubjectId,
                    Title = model.Title,
                });
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Please fill out the fields");

                return this.View(model);
            }

            return this.Json("/Subject/AddLesson");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("RemoveCategory")]
        public async Task<IActionResult> RemoveCategoryAsync(string categoryId)
        {
            if (categoryId == null) { return this.BadRequest("Please select category"); }

            await _subjectService.RemoveCategoryAsync(categoryId);

            return this.Redirect("/Subject/AddLesson");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("RemoveSubject")]
        public async Task<IActionResult> RemoveSubjectAsync(string subjectId)
        {
            if (subjectId == null) { return this.BadRequest("Please select subject"); }

            await _subjectService.RemoveSubjectAsync(subjectId);

            return this.Redirect("/Subject/AddLesson");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveLesson()
        {
            var categories = _subjectService.GetCategories().ToList();
            var lessons = _subjectService.GetLessons().ToList();

            return this.View(new LessonRemoveViewModel
            {
                Categories = categories,
                Lessons = lessons,
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("RemoveLesson")]
        public async Task<IActionResult> RemovelessonAsync(string lessonId)
        {
            try
            {
                await _subjectService.RemoveLessonAsync(lessonId);

                return this.Json("Topic was removed");
            }
            catch (Exception)
            {
                return this.Json("Error occurred while removing the topic");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin, Student")]
        [ActionName("LessonDetails")]
        public async Task<IActionResult> LessonDetailsAsync(string lessonId, string subjectId)
        {
            var lesson = await _subjectService.GetLessonAsync(lessonId);
            var lessonsViewModel = _mapper.Map<LessonsViewModel>(lesson);
            lessonsViewModel.SubjectId = subjectId;

            return this.View(lessonsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult LessonsPreview()
        {
            var lessons = _subjectService.GetLessons();

            var model = _mapper.Map<List<LessonPreviewViewModel>>(lessons);

            return this.View(model);
        }
    }
}