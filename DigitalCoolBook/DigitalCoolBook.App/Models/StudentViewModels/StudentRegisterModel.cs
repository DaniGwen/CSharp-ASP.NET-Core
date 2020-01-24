using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
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
        [StringLength(100, ErrorMessage = "Place of birth must be between 3 and 100 characters!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters!", MinimumLength = 1)]
        public string Sex { get; set; }

        [Display(Name = "Mobile Phone")]
        public int? MobilePhone { get; set; }

        [Required]
        public int Telephone { get; set; }

        [Display(Name = "Father's Name")]
        [StringLength(50, ErrorMessage = "Father name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        [StringLength(50, ErrorMessage = "Mother name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string MotherName { get; set; }

        [Display(Name = "Mother's Mobile Number")]
        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int? MotherMobileNumber { get; set; }

        [Display(Name = "Father's Mobile Number")]
        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int? FatherMobileNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Address must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Address { get; set; }
    }
}
