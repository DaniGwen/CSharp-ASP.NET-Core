namespace DigitalCoolBook.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subject
    {
        public Subject()
        {
            this.Categories = new List<Category>();
            this.Scores = new List<Score>();
            this.SubjectGrades = new List<SubjectGrade>();
        }

        [Key]
        public string SubjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public List<Category> Categories { get; set; }

        public List<Score> Scores{ get; set; }

        public List<SubjectGrade> SubjectGrades { get; set; }
    }
}