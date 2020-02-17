namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestStartViewModel
    {
        public string TestName { get; set; }

        [Display(Name = "Времетраене")]
        public TimeSpan Timer { get; set; }

        [Display(Name ="Дата")]
        public DateTime Date { get; set; }

        [Display(Name ="Въпроси")]
        public List<string> Questions { get; set; }
    }
}
