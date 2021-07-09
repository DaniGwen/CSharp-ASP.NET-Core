using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class TestStudent
    {
        public string Id { get; set; }

        public string TestId { get; set; }
        public Test Test { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
