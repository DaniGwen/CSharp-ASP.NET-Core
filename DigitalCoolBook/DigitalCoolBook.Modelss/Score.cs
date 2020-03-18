using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Score
    {
        public string ScoreId { get; set; }

        public int ScorePoints { get; set; }

        public string SubjectId { get; set; }

        public ICollection<ScoreStudent> ScoreRecords  { get; set; }
    }
}
