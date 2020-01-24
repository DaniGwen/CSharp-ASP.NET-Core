using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.TeacherViewModels
{
    public class TeacherChangePasswordViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
