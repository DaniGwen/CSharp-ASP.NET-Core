﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class GradeParalelo
    {
        public int Id { get; set; }

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