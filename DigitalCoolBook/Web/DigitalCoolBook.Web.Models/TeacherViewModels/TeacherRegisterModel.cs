using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.TeacherViewModels
{
    public class TeacherRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Place of Birth")]
        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters!", MinimumLength = 1)]
        public string Sex { get; set; }

        [Display(Name = "Mobile Phone")]
        public int? MobilePhone { get; set; }

        [Required]
        public int Telephone { get; set; }

        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Username { get; set; }
    }
}
