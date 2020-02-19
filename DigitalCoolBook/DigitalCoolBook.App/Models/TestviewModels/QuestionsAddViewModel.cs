namespace DigitalCoolBook.App.Models.TestviewModels
{
    using System.Collections.Generic;
    using DigitalCoolBook.App.Models.CategoryViewModels;

    public class QuestionsAddViewModel
    {
        public QuestionsAddViewModel()
        {
            this.Lessons = new List<LessonsViewModel>();
            this.Questions = new List<string>();
        }

        public string LessonId { get; set; }

        public List<string> Questions { get; set; }

        public List<LessonsViewModel> Lessons { get; set; }
    }
}