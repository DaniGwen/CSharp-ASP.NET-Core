using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Въведете имейл.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Въведете парола.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }

        public string Username { get; set; }
    }
}
