using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class SubjectGrade
    {
        public int Id { get; set; }

        [ForeignKey("Grade")]
        public string IdGrade { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("Subject")]
        public string IdSubject { get; set; }
        public Subject Subject { get; set; }
    }
}