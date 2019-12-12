using System.ComponentModel.DataAnnotations;

namespace Eventures.App.Models.BindingModels
{
    public class UserCreateBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must have more than 3 letters", MinimumLength = 3)]
        [RegularExpression("[A-Za-z0-9_`'-\\.]+")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name must have more than 3 letters", MinimumLength = 3)]
        [RegularExpression("[A-Za-z0-9_`'-\\.]+")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name must have more than 3 letters", MinimumLength = 3)]
        [RegularExpression("[A-Za-z0-9_`'-\\.]+")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        [Display(Name = "Unique Citizen Number")]
        public string UCN { get; set; }

    }
}
