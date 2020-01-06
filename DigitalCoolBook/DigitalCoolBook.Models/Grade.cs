﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalCoolBook.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [StringLength(1, ErrorMessage ="Name must be between 1 character!", MinimumLength = 1)]
        public string Name { get; set; }

        public ICollection<GradeParalelo> GradeParalelos { get; set; }

        public SubjectGrade SubjectGrade { get; set; }
    }
}