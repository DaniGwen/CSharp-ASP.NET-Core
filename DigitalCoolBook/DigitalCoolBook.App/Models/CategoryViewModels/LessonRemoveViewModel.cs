namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using DigitalCoolBook.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LessonRemoveViewModel
    {
        [Required]
        public string LessonId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public string LessonTitle { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<Category> Categories { get; set; }
    }
}
