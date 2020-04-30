using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Grade
    {
        public Grade()
        {
            this.GradeTeachers = new List<GradeTeacher>();
            this.Students = new List<Student>();
        }

        [Key]
        public string GradeId { get; set; }

        public string Name { get; set; }

        public ICollection<GradeTeacher> GradeTeachers { get; set; }

        public ICollection<Student> Students { get; set; }

        public SubjectGrade SubjectGrade { get; set; }
    }
}