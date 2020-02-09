namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Models.CategoryViewModels;
    using DigitalCoolBook.App.Models.SubjectViewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
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
        public async Task<IActionResult> CategoriesAsync(string id)
        {
            var categoryLessons = this.subjectService.GetLessons().ToList();
            var subjectsDb = this.subjectService.GetSubjects()
                .Include(s => s.Categories)
                .ToList();

            var subject = subjectsDb.FirstOrDefault(s => s.SubjectId == id);

            var model = this.mapper.Map<SubjectViewModel>(subject);

            return this.View(model);
        }

        [HttpGet]
        [ActionName("CategoryDetails")]
        public async Task<IActionResult> CategoryDetailsAsync(string id)
        {
            var lessons = this.subjectService.GetLessons()
                .Where(lesson => lesson.CategoryId == id)
                .ToList();

            var lessonsList = new List<LessonsViewModel>();

            foreach (var lesson in lessons)
            {
                var lessonDto = this.mapper.Map<LessonsViewModel>(lesson);
                lessonsList.Add(lessonDto);
            }

            return this.View(lessonsList);
        }

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

        [HttpPost]
        public IActionResult AddLesson(string categoryId, string content, string title)
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

            this.subjectService.CreateLessonAsync()

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
    }
}