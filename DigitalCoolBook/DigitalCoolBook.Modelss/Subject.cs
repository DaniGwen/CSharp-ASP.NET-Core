using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Subject
    {
        [Key]
        public string SubjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public ICollection<ScoreRecord> ScoreRecords { get; set; }

        public ICollection<SubjectGrade> SubjectGrades { get; set; }
    }
}