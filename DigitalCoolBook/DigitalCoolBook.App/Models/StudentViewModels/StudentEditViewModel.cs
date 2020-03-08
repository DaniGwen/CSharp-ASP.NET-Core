namespace DigitalCoolBook.App.Models.StudentViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.App.Models.GradesViewModels;
    using DigitalCoolBook.Models;

    public class StudentEditViewModel
    {
        public StudentEditViewModel()
        {
            this.Grades = new List<GradeViewModel>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Полето е задъжително.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задъжително.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Попълнете дата на раждане.")]
        public string DateOfBirth { get; set; }

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
