using System;

namespace DigitalCoolBook.Models
{
    public class LiveFeedMessage
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }
    }
}
