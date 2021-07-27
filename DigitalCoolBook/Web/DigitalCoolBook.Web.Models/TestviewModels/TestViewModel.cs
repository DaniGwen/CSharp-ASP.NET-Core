namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.Models;

    public class TestViewModel
    {
        public string TestId { get; set; }

        [Required(ErrorMessage ="Please enter test location")]
        public string Place { get; set; }

        [Required(ErrorMessage ="Please set test timer")]
        public int Timer { get; set; }

        public string TestName { get; set; }

        public DateTime Date { get; set; }

        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }

        public string[] Students { get; set; }

        public List<Test> Tests { get; set; }
    }
}
