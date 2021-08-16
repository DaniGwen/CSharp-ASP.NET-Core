using System.Collections.Generic;
using DigitalCoolBook.Models;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class QuestionsModel
    {
        public string QuestionId { get; set; }

        public string Title { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
