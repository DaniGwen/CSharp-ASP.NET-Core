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
        public int GradeParaleloId { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters!", MinimumLength = 1)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        [ForeignKey("Grade")]
        public int IdGrade { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("Teacher")]
        public int IdTeacher { get; set; }
        public Teacher Teacher { get; set; }
    }
}