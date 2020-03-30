using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class TestStudent
    {
        [Key]
        public string TestId { get; set; }
        public Test Test { get; set; }

        [Key]
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
