using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class GradeParalelo
    {
        public GradeParalelo()
        {
            this.Students = new List<Student>();
        }

        [Key]
        public string GradeParaleloId { get; set; }

        public ICollection<Student> Students { get; set; }

        [ForeignKey("Grade")]
        public string IdGrade { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("Teacher")]
        public string IdTeacher { get; set; }
        public Teacher Teacher { get; set; }
    }
}