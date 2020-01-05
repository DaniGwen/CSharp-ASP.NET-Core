using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class ScoreRecord
    {
        public int Id { get; set; }

        [ForeignKey("Subject")]
        public int IdSubject { get; set; }
        public Subject Subject { get; set; }


        [ForeignKey("Student")]
        public int IdStudent { get; set; }
        public Student Student { get; set; }
    }
}