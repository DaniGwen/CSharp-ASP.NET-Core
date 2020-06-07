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
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Името трябва да е между 3 и 50 символа!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата на раждане")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Място на раждане")]
        [StringLength(100, ErrorMessage = "Полето трябва да е между 3 и 100 символа!", MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = null, MinimumLength = 1)]
        public string Sex { get; set; }

        [Display(Name = "Мобилен номер")]
        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        [Display(Name = "Име на Баща")]
        [StringLength(50, ErrorMessage = "Полето трябва да е между 3 и 50 символа!", MinimumLength = 3)]
        public string FatherName { get; set; }

        [Display(Name = "Име на Майка")]
        [StringLength(50, ErrorMessage = "Полето трябва да е между 3 и 50 символа!", MinimumLength = 3)]
        public string MotherName { get; set; }

        [Display(Name = "Мобилен номер на Майката")]
        [Range(4, 20, ErrorMessage = "Минимум 4 и максимум 20 цифри!")]
        public int? MotherMobileNumber { get; set; }

        [Display(Name = "Мобилен номер на Бащата")]
        [Range(4, 20, ErrorMessage = "Минимум 4 и максимум 20 цифри!")]
        public int? FatherMobileNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Полето трябва да е между 3 и 50 символа!", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        public List<Grade> Grades { get; set; }

        public string GradeId { get; set; }
    }
}
