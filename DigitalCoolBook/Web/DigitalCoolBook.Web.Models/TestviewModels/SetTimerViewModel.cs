using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Web.Models.TestviewModels
{

    public class SetTimerViewModel
    {
        public string TestId { get; set; }

        public string TestName { get; set; }

        [Required(ErrorMessage ="Set timer")]
        public int Timer { get; set; }
    }
}
