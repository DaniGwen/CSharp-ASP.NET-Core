﻿using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.TestviewModels
{
    
    public class TestsNamesViewModel
    {
        [Display(Name ="Име")]
        public string TestName { get; set; }

        public string TestId { get; set; }
    }
}
