using DigitalCoolBook.App.Models.GradesViewModels;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentGradeEditViewModel
    {
        public StudentGradeEditViewModel()
        {
            this.Grades = new List<GradeViewModel>();
        }
        public StudentEditViewModel Student { get; set; }

        public List<GradeViewModel> Grades { get; set; }
    }
}
