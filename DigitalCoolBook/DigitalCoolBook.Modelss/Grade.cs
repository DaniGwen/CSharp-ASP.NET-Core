using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Grade
    {
        public Grade()
        {
            this.GradeParalelos = new List<GradeParalelo>();
        }

        [Key]
        public string GradeId { get; set; }

        [StringLength(3, ErrorMessage ="Name must be 3 character!")]
        public string Name { get; set; }

        public ICollection<GradeParalelo> GradeParalelos { get; set; }

        public SubjectGrade SubjectGrade { get; set; }
    }
}