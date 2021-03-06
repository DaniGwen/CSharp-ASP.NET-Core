﻿using DigitalCoolBook.Web.Models.SubjectViewModels;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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

    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IScoreService scoreService;

        public SubjectController(
            ISubjectService subjectService,
            IMapper mapper,
            IScoreService scoreService)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.scoreService = scoreService;
        }

        [HttpGet]
        public IActionResult Subjects()
        {
            var subjects = this.subjectService.GetSubjects().ToList();
            var subjectList = new List<SubjectViewModel>();

            foreach (var subject in subjects)
            {
                var subjectModel = this.mapper.Map<SubjectViewModel>(subject);
                subjectList.Add(subjectModel);
            }

            return this.View(subjectList);
        }

        [HttpGet]
        [ActionName("Categories")]
        public IActionResult CategoriesAsync(string subjectId, string categoryId)
        {
            //var categoryLessons = this.subjectService.GetLessons().ToList();

            var subjectDb = this.subjectService.GetSubjects()
                .Include(s => s.Categories)
                .FirstOrDefault(s => s.SubjectId == subjectId);

            var model = this.mapper.Map<SubjectViewModel>(subjectDb);

            return this.View(model);
        }

        [HttpGet]
        [ActionName("CategoryDetails")]
        [Authorize(Roles = "Admin, Teacher, Student")]
        public IActionResult CategoryDetailsAsync(CategoryDetailsViewModel categoryDetailsModel)
        {
            var lessons = this.subjectService.GetLessons()
                .Where(lesson => lesson.CategoryId == categoryDetailsModel.CategoryId)
                .ToList();

            var lessonsDto = this.mapper.Map<List<LessonsViewModel>>(lessons);

            // If User is Student adding score in view
            if (this.User.IsInRole("Student"))
            {
                foreach (var lesson in lessonsDto)
                {
                    // Gets student ID
                    var studentId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var score = this.scoreService
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
            var subjects = this.subjectService.GetSubjects().ToList();

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
                var lessonLevel = int.Parse(level);

                var lesson = new Lesson
                {
                    LessonId = Guid.NewGuid().ToString(),
                    CategoryId = categoryId,
                    Content = content,
                    Title = title,
                    Level = lessonLevel,
                };

                if (lessonLevel == 1)
                {
                    lesson.IsUnlocked = true;
                }

                await this.subjectService.CreateLessonAsync(lesson);

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
            var categories = this.subjectService
                .GetCategories()
                .Where(c => c.SubjectId == subjectId)
                .ToList();

            var categoriesDto = this.mapper
                .Map<IEnumerable<Category>, IEnumerable<CategoryAjaxViewModel>>(categories);

            return this.Json(categoriesDto);
        }

        [HttpPost]
        public JsonResult GetLessons(string categoryId)
        {
            var lessons = this.subjectService.GetLessons()
                .Where(l => l.CategoryId == categoryId);

            var model = this.mapper.Map<List<LessonsViewModel>>(lessons);

            return this.Json(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("EditLesson")]
        public async Task<IActionResult> EditLessonAsync(string id)
        {
            var lesson = await this.subjectService.GetLessonAsync(id);

            var model = this.mapper.Map<LessonEditViewModel>(lesson);

            var categories = this.subjectService.GetCategories().ToList();

            var categoriesDto = this.mapper.Map<IList<CategoryViewModel>>(categories);
            
            model.Categories = categoriesDto;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("EditLesson")]
        public async Task<IActionResult> EditLessonAsync(EditLessonPostViewModel viewModel)
        {
            try
            {
                var lesson = await this.subjectService.GetLessonAsync(viewModel.LessonId);

                lesson.Title = viewModel.Title;
                lesson.Content = viewModel.Content;
                lesson.CategoryId = viewModel.CategoryId;

                await this.subjectService.SaveChangesAsync();

                return this.Json("Changes are saved");
            }
            catch (Exception)
            {
                return this.Json("Error saving");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateSubject()
        {
            return this.View(new SubjectCreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSubjectAsync(SubjectCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.subjectService.GetSubjects().Any(s => s.Name == model.Name))
                {
                    this.ModelState.AddModelError(string.Empty, "Предмета вече съществува.");
                    return this.View(model);
                }

                var subject = new Subject
                {
                    Name = model.Name,
                    SubjectId = Guid.NewGuid().ToString(),
                };
                await this.subjectService.CreateSubjectAsync(subject);
            }
            else
            {
                return this.View(model);
            }

            this.TempData["SuccessMsg"] = "Предмета е създаден.";
            return this.Redirect("/Home/Success");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCategory()
        {
            var subjects = this.subjectService.GetSubjects().ToList();

            var model = new CategoryAdminCreateViewModel()
            {
                Subjects = this.mapper.Map<List<SubjectViewModel>>(subjects),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync(CategoryAdminCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    SubjectId = model.SubjectId,
                    Title = model.Title,
                };
                await this.subjectService.CreateCategoryAsync(category);
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Please fill out the fields");
                return this.View(model);
            }

            return this.Json("/Subject/AddLesson");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("RemoveCategory")]
        public async Task<IActionResult> RemoveCategoryAsync(string categoryId)
        {
            if (categoryId == null)
            {
                return this.BadRequest("Please select category");
            }

            await this.subjectService.RemoveCategoryAsync(categoryId);

            return this.Redirect("/Subject/AddLesson");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("RemoveSubject")]
        public async Task<IActionResult> RemoveSubjectAsync(string subjectId)
        {
            if (subjectId == null)
            {
                return this.BadRequest("Please select subject");
            }

            await this.subjectService.RemoveSubjectAsync(subjectId);

            return this.Redirect("/Subject/AddLesson");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveLesson()
        {
            var categories = this.subjectService.GetCategories().ToList();
            var lessons = this.subjectService.GetLessons().ToList();

            var model = new LessonRemoveViewModel
            {
                Categories = categories,
                Lessons = lessons,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("RemoveLesson")]
        public async Task<IActionResult> RemovelessonAsync(string lessonId)
        {
            try
            {
                await this.subjectService.RemoveLessonAsync(lessonId);

                return this.Json("Topic was removed");
            }
            catch (Exception)
            {
                return this.Json("Error removing topic");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin, Student")]
        [ActionName("LessonDetails")]
        public async Task<IActionResult> LessonDetailsAsync(string lessonId, string subjectId)
        {
            var lesson = await this.subjectService.GetLessonAsync(lessonId);
            var model = this.mapper.Map<LessonsViewModel>(lesson);
            model.SubjectId = subjectId;

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult LessonsPreview()
        {
            var lessons = this.subjectService.GetLessons();

            var model = this.mapper.Map<List<LessonPreviewViewModel>>(lessons);

            return this.View(model);
        }
    }
}