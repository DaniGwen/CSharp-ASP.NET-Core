using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Teacher
    {
        [Key]
        public string TeacherId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Name must be between 3 and 60 characters!", MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters!", MinimumLength = 1)]
        public string Sex { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int MobilePhone { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int Telephone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

       
        public string Username { get; set; }
      
    }
}