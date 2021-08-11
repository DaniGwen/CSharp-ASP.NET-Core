using System;

namespace DigitalCoolBook.Models
{
    public class LiveFeedMessage
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public DateTime Date { get; set; }
    }
}
