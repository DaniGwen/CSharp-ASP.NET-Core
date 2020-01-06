using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters!", MinimumLength = 1)]
        public string Sex { get; set; }

        public int MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string Password { get; set; }
    }
}