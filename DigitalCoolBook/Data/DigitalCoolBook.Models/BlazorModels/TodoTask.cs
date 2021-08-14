using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DigitalCoolBook.Models.BlazorModels
{
    public class TodoTask
    {
        public TodoTask()
        {
            this.Todos = new List<Todo>();
        }

        [Key]
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }
    }
}
