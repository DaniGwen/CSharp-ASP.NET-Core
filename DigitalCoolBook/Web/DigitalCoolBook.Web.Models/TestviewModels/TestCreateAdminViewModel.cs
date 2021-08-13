using DigitalCoolBook.Web.Models.CategoryViewModels;

namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.App.Models.CategoryViewModels;

    public class TestCreateAdminViewModel
    {
        [Required(ErrorMessage = "Въведете място.")]
        public string Place { get; set; }

        [Required(ErrorMessage="Изберете тема.")]
        public string LessonId { get; set; }

        public List<LessonsViewModel> Lessons { get; set; }

        public string[] Questions { get; set; }

        public string[] Answers { get; set; }

    }
}
