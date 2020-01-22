using System.Collections.Generic;

namespace DigitalCoolBook.App.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            this.Messages = new List<string>();
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public List<string> Messages { get; set; }
    }
}
