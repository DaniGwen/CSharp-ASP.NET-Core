﻿namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.App.Models.CategoryViewModels;
    using DigitalCoolBook.App.Models.StudentViewModels;
    using DigitalCoolBook.Models;

    public class TestViewModel
    {
        public TestViewModel()
        {
            this.Grades = new List<Grade>();
            this.Students = new List<StudentTestDropDownModel>();
            this.Lessons = new List<LessonsViewModel>();
        }

        public string TestId { get; set; }

        [Display(Name ="Място")]
        [Required(ErrorMessage ="Моля въведете място на провеждане на теста.")]
        public string Place { get; set; }

        [Required(ErrorMessage ="Моля попълнете дата.")]
        [Display(Name ="Дата и час на теста")]
        public DateTime Timer { get; set; }

        [Display(Name = "Дата на провеждане")]
        public DateTime Date { get; set; }

        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }

        public string LessonId { get; set; }
        public List<LessonsViewModel> Lessons { get; set; }

        public string StudentId { get; set; }

        public List<StudentTestDropDownModel> Students { get; set; }
    }
}
