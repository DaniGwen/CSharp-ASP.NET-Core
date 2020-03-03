namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.Models;

    public class TestViewModel
    {
        public string TestId { get; set; }

        [Display(Name ="Място")]
        [Required(ErrorMessage ="Моля въведете място на провеждане на теста.")]
        public string Place { get; set; }

        [Required(ErrorMessage ="Въведете времетраене на теста.")]
        [Display(Name ="Времетраене")]
        public int Timer { get; set; }

        [Display(Name = "Дата на провеждане")]
        public DateTime Date { get; set; }

        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }

        public string TestTitle { get; set; }

        public List<Test> Tests { get; set; }
    }
}
