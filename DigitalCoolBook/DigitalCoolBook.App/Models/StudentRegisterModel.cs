﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.App.Models
{
    public class StudentRegisterModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters!", MinimumLength = 1)]
        public string Sex { get; set; }

        public int MobilePhone { get; set; }

        public int Telephone { get; set; }

        [StringLength(50, ErrorMessage = "Father name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string FatherName { get; set; }

        [StringLength(50, ErrorMessage = "Mother name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string MotherName { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int MotherMobileNumber { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int FatherMobileNumber { get; set; }

        [StringLength(50, ErrorMessage = "Address must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Address { get; set; }
    }
}
