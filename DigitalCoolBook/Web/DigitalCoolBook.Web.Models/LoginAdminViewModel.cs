using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class LoginAdminViewModel
    {
        [Required(ErrorMessage ="Въведете потребителско име.")]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Въведете парола.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}
