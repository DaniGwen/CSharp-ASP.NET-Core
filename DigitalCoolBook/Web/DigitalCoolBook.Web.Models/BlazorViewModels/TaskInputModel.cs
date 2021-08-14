using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Web.Models.BlazorViewModels
{
    public class TaskInputModel
    {
        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }
    }
}
