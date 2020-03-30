﻿namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;

    public class CategoryDetailsViewModel
    {
        public List<LessonsViewModel> Lessons { get; set; }

        public string SubjectId { get; set; }

        public string CategoryTitle { get; set; }
    }
}