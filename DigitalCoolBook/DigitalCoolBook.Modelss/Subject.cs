namespace DigitalCoolBook.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Subject
    {
        [Key]
        public string SubjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public List<Category> Categories { get; set; }

        public List<ScoreRecord> ScoreRecords { get; set; }

        public List<SubjectGrade> SubjectGrades { get; set; }
    }
}