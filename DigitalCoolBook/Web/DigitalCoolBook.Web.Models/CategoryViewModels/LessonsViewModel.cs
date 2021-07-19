namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    public class LessonsViewModel
    {
        public string CategoryId { get; set; }

        public string SubjectId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string LessonId { get; set; }

        public int Score { get; set; }

        public bool IsUnlocked { get; set; }
    }
}
