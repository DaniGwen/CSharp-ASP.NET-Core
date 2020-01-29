﻿using DigitalCoolBook.Models;
using System.Collections.Generic;

namespace DigitalCoolBook.App.Models.GradesViewModels
{
    public class GradeDetailViewModel
    {
        public string Name { get; set; }

        public List<ScoreRecord> ScoreRecords { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}