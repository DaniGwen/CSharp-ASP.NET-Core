namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TestStartViewModel
    {
        public string TestName { get; set; }

        [Display(Name = "Времетраене")]
        public string Timer { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Въпроси")]
        public List<QuestionsModel> Questions { get; set; }

        public bool IsExpired { get; set; }

        public string[] Answers { get; set; }
    }
}
