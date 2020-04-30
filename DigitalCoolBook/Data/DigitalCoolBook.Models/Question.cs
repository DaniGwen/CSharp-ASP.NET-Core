using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
        }

        [Key]
        public string QuestionId { get; set; }

        public string Title { get; set; }

        public string TestId { get; set; }

        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
