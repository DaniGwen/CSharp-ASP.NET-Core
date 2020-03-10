namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System.Collections.Generic;

    public class QuestionDetailsViewModel
    {
        public string Title { get; set; }

        public string QuestionId { get; set; }

        public List<AnswerDetailsViewModel> Answers { get; set; }
    }
}
