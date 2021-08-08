using System.Collections.Generic;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class TestSummaryViewModel
    {
        public string TestId { get; set; }

        public string TestName { get; set; }

        public List<StudentTestSummaryViewModel> ParticipatedStudents { get; set; } = new List<StudentTestSummaryViewModel>();
    }
}
