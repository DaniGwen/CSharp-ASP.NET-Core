using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class LoginAdminViewModel
    {
        [Required(ErrorMessage ="Моля въведете потребителско име")]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
