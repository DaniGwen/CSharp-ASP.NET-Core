using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models.BlazorModels
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsDone { get; set; } = false;

        [ForeignKey(nameof(TodoTask))]
        public int TodoTaskId { get; set; }

        public virtual TodoTask TodoTask{ get; set; }
    }
}
