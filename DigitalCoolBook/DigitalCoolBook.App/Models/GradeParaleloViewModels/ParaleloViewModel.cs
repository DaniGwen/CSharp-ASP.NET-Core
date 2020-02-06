namespace DigitalCoolBook.App.Models.GradeParaleloViewModels
{
    using System.Collections.Generic;
    using DigitalCoolBook.Models;

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
