
using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionCreatViewModel
    {
        [StringLengthSis(30, 800, "Code must be between 30 and 800 characters")]
        public string Code { get; set; }

        public string ProblemId { get; set; }
    }
}
