using DigitalCoolBook.App.Models.GradesViewModels;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.StudentViewModels
{
    public class StudentGradeEditViewModel
    {
        public List<StudentEditViewModel> Students { get; set; }

        public List<GradeViewModel> Grades { get; set; }
    }
}
