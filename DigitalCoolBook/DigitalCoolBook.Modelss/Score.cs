using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Score
    {
        public string ScoreId { get; set; }

        public int ScorePoints { get; set; }

        public string LessonId { get; set; }

        public ICollection<ScoreStudent> ScoreStudents  { get; set; }
    }
}
