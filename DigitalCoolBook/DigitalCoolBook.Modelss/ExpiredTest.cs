using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class ExpiredTest
    {
        [Key]
        public string ExpiredTestId { get; set; }

        public string TestName { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int Timer { get; set; }

        public string LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime Date { get; set; }

        public int Result { get; set; }

        public string Place { get; set; }

        public virtual ICollection<TestStudent> TestStudent { get; set; }

        public ICollection<Question> Questions { get; set; }

        public bool IsExpired { get; set; }
    }
}
