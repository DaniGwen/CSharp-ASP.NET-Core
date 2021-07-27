namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;

    public class TestStartViewModel
    {
        public string TestName { get; set; }

        public string TestId { get; set; }

        public string Timer { get; set; }

        public DateTime Date { get; set; }

        public List<QuestionsModel> Questions { get; set; }

        public bool IsExpired { get; set; }

        public string[] Answers { get; set; }

        public List<string> StudentNames { get; set; }
    }
}
