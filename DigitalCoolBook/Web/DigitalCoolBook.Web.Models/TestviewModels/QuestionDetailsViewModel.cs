namespace DigitalCoolBook.App.Models.TestviewModels
{
    using AutoMapper;
    using System.Collections.Generic;

    public class QuestionDetailsViewModel
    {
        public string Title { get; set; }

        public string QuestionId { get; set; }

        [IgnoreMap]
        public List<AnswerDetailsViewModel> Answers { get; set; }
    }
}
