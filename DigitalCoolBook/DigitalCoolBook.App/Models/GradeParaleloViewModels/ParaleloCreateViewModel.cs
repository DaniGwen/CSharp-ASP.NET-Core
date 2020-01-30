using DigitalCoolBook.Models;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.GradeParaleloViewModels
{
    public class ParaleloCreateViewModel
    {
        public string Id { get; set; }

        public string GradeId { get; set; }

        public string GradeName { get; set; }

        public string TeacherId { get; set; }

        public string TeacherName { get; set; }

        public List<Grade> Grades { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}
