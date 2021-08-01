using System;
using System.Collections.Generic;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class ArchivedTestViewModel
    {
        public Guid Id { get; set; }

        public string TestName { get; set; }

        public string TestId { get; set; }

        public int Timer { get; set; }

        public string TeacherId { get; set; }

        public string LessonId { get; set; }

        public DateTime Date { get; set; }

        public string Place { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
