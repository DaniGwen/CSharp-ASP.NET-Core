namespace DigitalCoolBook.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CorrectAnswer
    {
        [Key]
        public string CorrectAnswerId { get; set; }

        public string AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
