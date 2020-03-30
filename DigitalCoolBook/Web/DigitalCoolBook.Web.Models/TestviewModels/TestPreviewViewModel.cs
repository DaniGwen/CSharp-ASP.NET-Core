using System.ComponentModel;

namespace DigitalCoolBook.App.Models.TestviewModels
{
    public class TestPreviewViewModel
    {
        [DisplayName("Тема")]
        public string TestName { get; set; }

        public string TestId { get; set; }
    }
}
