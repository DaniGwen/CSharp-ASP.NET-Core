using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public ICollection<ScoreRecord> ScoreRecords { get; set; }

        public ICollection<SubjectGrade> SubjectGrades { get; set; }
    }
}