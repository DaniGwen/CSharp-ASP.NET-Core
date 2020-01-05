using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Sex { get; set; }

        public int MobilePhone { get; set; }

        public string Address { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public int MotherMobileNumber { get; set; }

        public int FatherMobileNumber { get; set; }

        public int Telephone { get; set; }

        public string Password { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        [ForeignKey("GradeParalelo")]
        public int IdGradeParalelo { get; set; }
        public GradeParalelo GradeParalelo { get; set; }

        public ICollection<ScoreRecord> ScoreRecords { get; set; }

    }
}
