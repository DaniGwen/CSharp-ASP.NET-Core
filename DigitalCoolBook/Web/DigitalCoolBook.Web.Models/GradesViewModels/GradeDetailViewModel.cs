using DigitalCoolBook.Models;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.GradesViewModels
{
    public class GradeDetailViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ScoreStudent> ScoreRecords { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}
