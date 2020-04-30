namespace DigitalCoolBook.Models
{
    using System;
    using System.Collections.Generic;

    public class Test
    {
        public Test()
        {
            this.TestStudent = new List<TestStudent>();
            this.Questions = new List<Question>();
        }

        public string TestId { get; set; }

        public string TestName { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int Timer { get; set; }

        public string LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime Date { get; set; }

        public int Result { get; set; }

        public string Place { get; set; }

        public ICollection<TestStudent> TestStudent { get; set; }

        public ICollection<Question> Questions { get; set; }

        public bool IsExpired { get; set; }
    }
}
