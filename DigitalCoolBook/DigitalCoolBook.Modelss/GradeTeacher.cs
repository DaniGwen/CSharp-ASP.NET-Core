using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class GradeTeacher
    {
        [Key]
        public string GradeTeacherId { get; set; }

        public List<Student> Students { get; set; }

        public string IdGrade { get; set; }
        public Grade Grade { get; set; }

        public string IdTeacher { get; set; }
        public Teacher Teacher { get; set; }
    }
}