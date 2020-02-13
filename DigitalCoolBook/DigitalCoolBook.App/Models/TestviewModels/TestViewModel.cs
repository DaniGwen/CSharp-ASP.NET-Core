namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestViewModel
    {
        public string TestId { get; set; }

        public string Place { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }
    }
}
