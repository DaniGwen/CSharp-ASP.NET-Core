using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Attendance
    {
        [Key]
        public string AttendanceId { get; set; }

        public DateTime Date { get; set; }

        public bool Attended { get; set; }

        [ForeignKey("Student")]
        public string IdStudent { get; set; }
        public Student Student { get; set; }
    }
}