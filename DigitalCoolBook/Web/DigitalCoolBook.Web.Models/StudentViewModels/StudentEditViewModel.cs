using System;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using GradesViewModels;
    using DigitalCoolBook.Models;

    public class StudentEditViewModel
    {
        public StudentEditViewModel()
        {
            this.Grades = new List<GradeViewModel>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Sex { get; set; }

        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public int? MotherMobileNumber { get; set; }

        public int? FatherMobileNumber { get; set; }

        public string Address { get; set; }

        public Grade Grade { get; set; }

        public string GradeId { get; set; }

        public List<GradeViewModel> Grades { get; set; }

        public string GradeName { get; set; }
    }
}
