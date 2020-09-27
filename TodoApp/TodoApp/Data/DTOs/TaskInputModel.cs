using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.DTOs
{
    public class TaskInputModel
    {
        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }
    }
}
