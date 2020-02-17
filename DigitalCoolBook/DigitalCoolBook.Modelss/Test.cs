namespace DigitalCoolBook.Models
{
    using System;
    using System.Collections.Generic;

    public class Test
    {
        public Test()
        {
            this.TestStudent = new HashSet<TestStudent>();
        }

        public string TestId { get; set; }

        public string TestName { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public TimeSpan Timer { get; set; }

        public string LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime Date { get; set; }

        public int Result { get; set; }

        public string Place { get; set; }

        public virtual ICollection<TestStudent> TestStudent { get; set; }

        public string GradeId { get; set; }
    }
}
