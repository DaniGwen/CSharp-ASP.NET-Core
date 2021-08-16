using System;
using System.Collections.Generic;
using DigitalCoolBook.App.Models.TestviewModels;
using DigitalCoolBook.Web.Models.TestviewModels;

namespace DigitalCoolBook.Web.Models.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public int? MotherMobileNumber { get; set; }

        public int? FatherMobileNumber { get; set; }

        public string Address { get; set; }

        public string GradeId { get; set; }

        public List<TestStudentDetailsViewModel> TestsTaken { get; set; } = new();

        public double AverageScore { get; set; }
    }
}
