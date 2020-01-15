using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class ScoreRecord
    {
        public int Id { get; set; }

        [ForeignKey("Subject")]
        public string IdSubject { get; set; }
        public Subject Subject { get; set; }


        [ForeignKey("Student")]
        public string IdStudent { get; set; }
        public Student Student { get; set; }
    }
}