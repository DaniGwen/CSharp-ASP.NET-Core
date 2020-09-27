using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Data.Models
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey(nameof(TodoTask))]
        public int TodoTaskId { get; set; }

        public virtual TodoTask TodoTask{ get; set; }
    }
}
