namespace DigitalCoolBook.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Lesson
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Contend { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}