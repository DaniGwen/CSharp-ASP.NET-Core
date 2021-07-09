using DigitalCoolBook.Models;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.GradeParaleloViewModels
{
    public class ParaleloCreateViewModel
    {
        public string GradeParaleloId { get; set; }

        public string IdGrade { get; set; }

        public string GradeName { get; set; }

        public string IdTeacher { get; set; }

        public string TeacherName { get; set; }

        public List<Grade> Grades { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}
