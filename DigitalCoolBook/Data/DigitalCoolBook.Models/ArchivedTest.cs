using System;
using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class ArchivedTest
    {
        public Guid Id { get; set; }

        public string TestName { get; set; }

        public string TestId { get; set; }

        public int Timer { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime Date { get; set; }

        public string Place { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
