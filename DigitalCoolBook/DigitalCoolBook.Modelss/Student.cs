using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Student
    {
        public Student()
        {
            this.ScoreRecords = new List<ScoreRecord>();
            this.Attendances = new List<Attendance>();
        }

        [Key]
        public string StudentId { get; set; }

        [StringLength(60, ErrorMessage = "Email must be between 3 and 60 characters!", MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200, ErrorMessage = "Characters must be between 3 and 200!")]
        public string PlaceOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Sex must be between 1 and 20 characters!")]
        public string Sex { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int MobilePhone { get; set; }

        [StringLength(100, ErrorMessage = "Address length must be less than 100!")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string FatherName { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string MotherName { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int MotherMobileNumber { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int FatherMobileNumber { get; set; }

        [Range(4, 20, ErrorMessage = "Enter between 4 and 20 digits!")]
        public int Telephone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        [ForeignKey("GradeParalelo")]
        public string IdGradeParalelo { get; set; }
        public GradeParalelo GradeParalelo { get; set; }

        public ICollection<ScoreRecord> ScoreRecords { get; set; }

        public string Username { get; set; }
     

    }
}
