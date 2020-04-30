using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class GradeTeacher
    {
        public GradeTeacher()
        {
            this.Students = new List<Student>();
        }

        [Key]
        public string GradeTeacherId { get; set; }

        public ICollection<Student> Students { get; set; }

        public string GradeId { get; set; }
        public Grade Grade { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}