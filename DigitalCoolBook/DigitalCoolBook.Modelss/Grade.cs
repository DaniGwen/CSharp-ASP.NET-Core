using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Grade
    {
        [Key]
        public string GradeId { get; set; }

        [StringLength(3, ErrorMessage ="Name must be 3 character!")]
        public string Name { get; set; }

        public List<GradeParalelo> GradeParalelos { get; set; }

        public SubjectGrade SubjectGrade { get; set; }

        public List<Student> Students { get; set; }
    }
}