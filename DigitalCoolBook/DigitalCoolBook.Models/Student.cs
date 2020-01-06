using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200,ErrorMessage ="Characters must be between 3 and 200!")]
        public string PlaceOfBirth { get; set; }

        [StringLength(20,ErrorMessage ="Sex must be between 1 and 20 characters!")]
        public string Sex { get; set; }

        [MaxLength(20)]
        public int MobilePhone { get; set; }

        [StringLength(100,ErrorMessage ="Address length must be less than 100!")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
        public string FatherName { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 3)]
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
