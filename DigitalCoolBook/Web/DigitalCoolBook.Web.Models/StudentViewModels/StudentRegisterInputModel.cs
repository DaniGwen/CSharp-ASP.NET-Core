using DigitalCoolBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentRegisterInputModel
    {
        public StudentRegisterInputModel()
        {
            this.Grades = new List<Grade>();
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Place of birth")]
        [StringLength(100, ErrorMessage = "Field must be between 3 and 100 characters", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = null, MinimumLength = 1)]
        public string Sex { get; set; }

        [Display(Name = "Mobile number")]
        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        [Display(Name = "Name of father")]
        [StringLength(50, ErrorMessage = "Field must be between 3 and 50 characters", MinimumLength = 3)]
        public string FatherName { get; set; }

        [Display(Name = "Name of mother")]
        [StringLength(50, ErrorMessage = "Field must be between 3 and 50 characters", MinimumLength = 3)]
        public string MotherName { get; set; }

        [Display(Name = "Mobile number of mother")]
        public int? MotherMobileNumber { get; set; }

        [Display(Name = "Mobile number of father")]
        public int? FatherMobileNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Address must be between 3 and 50 characters", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }
    }
}
