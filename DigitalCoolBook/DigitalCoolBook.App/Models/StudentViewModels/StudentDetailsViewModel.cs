using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public string StudentId { get; set; }

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
    }
}
