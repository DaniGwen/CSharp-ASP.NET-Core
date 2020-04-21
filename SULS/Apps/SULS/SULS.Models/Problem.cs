﻿using System.Collections.Generic;

namespace SULS.Models
{
    public class Problem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public virtual List<Submission> Submitions { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}