﻿namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models;
    using DigitalCoolBook.App.Models.CategoryViewModels;
    using DigitalCoolBook.App.Models.SubjectViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
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
        public IActionResult CategoriesAsync(string Id)
        {
            var categoryLessons = this.subjectService.GetLessons().ToList();
            var subjectsDb = this.subjectService.GetSubjects()
                .Include(s => s.Categories)
                .ToList();

            var subject = subjectsDb.FirstOrDefault(s => s.SubjectId == Id);

            var model = this.mapper.Map<SubjectViewModel>(subject);

            return this.View(model);
        }

        [HttpGet]
        [ActionName("CategoryDetails")]
        public IActionResult CategoryDetailsAsync(string categoryId, string categoryTitle, string subjectId)
        {
            var lessons = this.subjectService.GetLessons()
                .Where(lesson => lesson.CategoryId == categoryId)
                .ToList();

            var lessonsList = new List<LessonsViewModel>();

            foreach (var lesson in lessons)
            {
                var lessonDto = this.mapper.Map<LessonsViewModel>(lesson);
                lessonsList.Add(lessonDto);
            }

            this.ViewData["SubjectID"] = subjectId;
            this.ViewData["categoryTitle"] = categoryTitle;

            return this.View(lessonsList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddLesson()
        {
            var categories = this.subjectService.GetCategories().ToList();
            var subjects = this.subjectService.GetSubjects().ToList();

            var categoriesModel = new CategoryCreateViewModel
            {
                Categories = categories,
                Subjects = subjects,
            };

            return this.View(categoriesModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("AddLesson")]
        public async Task<IActionResult> AddLessonAsync(string categoryId, string content, string title)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var lesson = new Lesson
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = categoryId,
                Content = content,
                Title = title,
            };

            await this.subjectService.CreateLessonAsync(lesson);

            return this.Redirect("/Home/SuccessfulySaved");
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> EditLessonAsync(string id)
        {
            var lesson = await this.subjectService.GetLessonAsync(id);

            var model = this.mapper.Map<LessonEditViewModel>(lesson);

            var categories = this.subjectService.GetCategories().ToList();

            var categoriesDto = this.mapper.Map<IList<CategoryViewModel>>(categories);

            model.Categories = categoriesDto;

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditLessonAsync(LessonEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var lesson = await this.subjectService.GetLessonAsync(model.Id);
                lesson.Title = model.Title;
                lesson.Content = model.Content;
                lesson.CategoryId = model.CategoryId;

                await this.subjectService.SaveChangesAsync();
            }
            else
            {
                return this.View(model);
            }

            return this.Redirect("/Home/SuccessfulySaved");
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

            return this.Redirect("/Home/SuccessfulySaved");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            var subjects = this.subjectService.GetSubjects().ToList();

            var model = new CategoryAdminCreateViewModel()
            {
                Subjects = this.mapper.Map<List<SubjectViewModel>>(subjects),
            };

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
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
                this.ModelState.AddModelError(string.Empty, "Моля попълнете всички полета.");
                return this.View(model);
            }

            return this.Redirect("/Subject/AddLesson");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("RemoveCategory")]
        public async Task<IActionResult> RemoveCategoryAsync(string categoryId)
        {
            if (categoryId == null)
            {
                return this.BadRequest("Моля изберете категория.");
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
                return this.BadRequest("Моля изберете предмет.");
            }

            await this.subjectService.RemoveSubjectAsync(subjectId);

            return this.Redirect("/Subject/AddLesson");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("RemoveLesson")]
        public async Task<IActionResult> RemovelessonAsync(string lessonId)
        {
            if (lessonId == null)
            {
                return this.BadRequest("Моля изберете урок.");
            }

            await this.subjectService.RemoveLessonAsync(lessonId);

            return this.Redirect("/Subject/AddLesson");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        [ActionName("LessonDetails")]
        public async Task<IActionResult> LessonDetailsAsync(string id)
        {
            var lesson = await this.subjectService.GetLessonAsync(id);

            var model = this.mapper.Map<LessonsViewModel>(lesson);

            return this.View(model);
        }
    }
}