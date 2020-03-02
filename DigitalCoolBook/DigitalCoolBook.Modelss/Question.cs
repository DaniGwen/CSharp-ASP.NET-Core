using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Question
    {
        public string QuestionId { get; set; }

        public string Content { get; set; }

        public string TestId { get; set; }

        public Test Test { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
