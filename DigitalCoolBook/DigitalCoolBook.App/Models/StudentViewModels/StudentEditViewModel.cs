using DigitalCoolBook.Models;
using System;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentEditViewModel
    {
        public string StudentId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

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
    }
}
