using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool Attended { get; set; }

        [ForeignKey("Student")]
        public int IdStudent { get; set; }
        public Student Student { get; set; }
    }
}