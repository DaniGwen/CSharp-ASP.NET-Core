using DigitalCoolBook.Models;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.GradeParaleloViewModels
{
    public class ParaleloViewModel
    {
        public string Id { get; set; }

        public string TeacherId { get; set; }

        public string TeacherName { get; set; }

        public string GradeId { get; set; }

        public string GradeName { get; set; }

        public List<Student> Students { get; set; }
    }
}
