using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Web.Models.SubjectViewModels
{
    public class EditLessonPostViewModel
    {
        public string LessonId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
    }
}
