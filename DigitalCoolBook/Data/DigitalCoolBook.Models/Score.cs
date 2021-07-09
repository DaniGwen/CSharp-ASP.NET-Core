using System;
using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Score
    {
        public Score()
        {
            this.ScoreId = Guid.NewGuid().ToString();
            this.ScoreStudents = new LinkedList<ScoreStudent>();
        }

        public string ScoreId { get; set; }

        public int ScorePoints { get; set; }

        public string LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<ScoreStudent> ScoreStudents  { get; set; }
    }
}
