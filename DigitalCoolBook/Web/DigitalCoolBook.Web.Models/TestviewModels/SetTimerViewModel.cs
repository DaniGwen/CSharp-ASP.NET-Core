using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.TestviewModels
{

    public class SetTimerViewModel
    {
        public string TestId { get; set; }

        public string TestName { get; set; }

        [Required(ErrorMessage ="Задайте време.")]
        public int Timer { get; set; }
    }
}
