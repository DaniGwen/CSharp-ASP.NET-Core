using DigitalCoolBook.Web.Models.TestviewModels;

namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System.Collections.Generic;

    public class TestDetailsViewModel
    {
        public string TestId { get; set; }

        public string TestName { get; set; }

        public List<QuestionDetailsViewModel> Questions { get; set; }
    }
}
