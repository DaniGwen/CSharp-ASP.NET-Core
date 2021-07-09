namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System.Collections.Generic;
    using DigitalCoolBook.Models;

    public class QuestionsModel
    {
        public string QuestionId { get; set; }

        public string Title { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
