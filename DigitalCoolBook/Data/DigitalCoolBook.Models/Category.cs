using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Category
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }

        public List<Lesson> Lessons { get; set; }


    }
}