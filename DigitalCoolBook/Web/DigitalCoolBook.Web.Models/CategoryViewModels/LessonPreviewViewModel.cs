namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.ComponentModel;

    public class LessonPreviewViewModel
    {
        public string LessonId { get; set; }

        [DisplayName("Заглавие")]
        public string Title { get; set; }
    }
}
