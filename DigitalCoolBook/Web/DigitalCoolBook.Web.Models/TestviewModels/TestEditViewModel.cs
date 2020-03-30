using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.TestviewModels
{
    public class TestEditViewModel
    {
        public string QuestionId { get; set; }

        public string Question { get; set; }

        public ICollection<string> Answers { get; set; }
    }
}
