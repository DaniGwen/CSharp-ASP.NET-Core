using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class SubjectViewModel
    {
        [Key]
        public string SubjectId { get; set; }

        public string Name { get; set; }
    }
}
