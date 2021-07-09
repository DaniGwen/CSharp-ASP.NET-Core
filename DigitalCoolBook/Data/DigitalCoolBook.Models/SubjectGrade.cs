using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class SubjectGrade
    {
        public int Id { get; set; }

        //[ForeignKey("Grade")]
        public string GradeId { get; set; }
        public Grade Grade { get; set; }

        //[ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}