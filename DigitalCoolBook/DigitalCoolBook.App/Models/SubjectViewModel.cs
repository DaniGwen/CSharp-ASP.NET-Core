using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class SubjectViewModel
    {
        [Key]
        public int SubjectId { get; set; }

        public string Name { get; set; }
    }
}
