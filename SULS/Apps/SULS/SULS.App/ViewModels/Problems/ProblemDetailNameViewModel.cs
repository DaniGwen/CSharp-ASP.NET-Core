using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailNameViewModel
    {
        public string Name { get; set; }

        public List<ProblemDetailViewModel> Submissions { get; set; }
    }
}
