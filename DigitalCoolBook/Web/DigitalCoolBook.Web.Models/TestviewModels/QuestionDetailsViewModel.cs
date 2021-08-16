using System.Collections.Generic;
using AutoMapper;
using DigitalCoolBook.App.Models.TestviewModels;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class QuestionDetailsViewModel
    {
        public string Title { get; set; }

        public string QuestionId { get; set; }

        [IgnoreMap]
        public List<AnswerDetailsViewModel> Answers { get; set; }
    }
}
