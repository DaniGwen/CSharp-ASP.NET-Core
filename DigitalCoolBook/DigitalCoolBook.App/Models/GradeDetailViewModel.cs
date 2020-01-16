using DigitalCoolBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalCoolBook.App.Models
{
    public class GradeDetailViewModel
    {
        public string Name { get; set; }

        public List<ScoreRecord> ScoreRecords { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}
