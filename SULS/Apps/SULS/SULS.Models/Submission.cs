﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Code { get; set; }

        public int AchievedResult { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Problem Problem { get; set; }
        public string ProblemId { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
