using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<GradeParalelo> GradeParalelos { get; set; }

        public SubjectGrade SubjectGrade { get; set; }
    }
}