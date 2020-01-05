using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public ICollection<ScoreRecord> ScoreRecords { get; set; }

        public ICollection<SubjectGrade> SubjectGrades { get; set; }
    }
}