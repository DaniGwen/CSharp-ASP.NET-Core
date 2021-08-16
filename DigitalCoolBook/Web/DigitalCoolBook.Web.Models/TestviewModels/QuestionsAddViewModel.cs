using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalCoolBook.Web.Models.CategoryViewModels;

namespace DigitalCoolBook.Web.Models.TestviewModels
{
    public class QuestionsAddViewModel
    {
        public QuestionsAddViewModel()
        {
            this.Lessons = new List<LessonsViewModel>();
            this.Questions = new List<string>();
        }

        [Required(ErrorMessage = "Please select from the menu")]
        public string LessonId { get; set; }

        public List<string> Questions { get; set; }

        public List<LessonsViewModel> Lessons { get; set; }
    }
}