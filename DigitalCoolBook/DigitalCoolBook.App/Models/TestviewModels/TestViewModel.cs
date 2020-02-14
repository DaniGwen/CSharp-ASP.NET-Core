namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using DigitalCoolBook.Models;

    public class TestViewModel
    {
        public string TestId { get; set; }

        public string Place { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }

        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }

        public string LessonId { get; set; }
    }
}
