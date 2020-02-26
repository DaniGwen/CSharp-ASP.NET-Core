namespace DigitalCoolBook.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Lesson
    {
        [Key]
        public string LessonId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}