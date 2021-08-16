using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalCoolBook.Web.Models.CategoryViewModels;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class TestCreateAdminViewModel
    {
        [Required(ErrorMessage = "Enter place")]
        public string Place { get; set; }

        [Required(ErrorMessage="Choose lesson")]
        public string LessonId { get; set; }

        public List<LessonsViewModel> Lessons { get; set; }

        public string[] Questions { get; set; }

        public string[] Answers { get; set; }

    }
}
