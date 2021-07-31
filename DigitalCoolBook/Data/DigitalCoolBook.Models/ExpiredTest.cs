using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class ExpiredTest
    {
        public ExpiredTest()
        {
            this.ExpiredTestId = Guid.NewGuid().ToString();
            this.CorrectAnswers = new List<CorrectAnswer>();
        }

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

        public string StudentId { get; set; }

        public ICollection<CorrectAnswer> CorrectAnswers { get; set; }
    }
}
