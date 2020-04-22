using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Models;
using SULS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;

        public ProblemsController(IProblemService problemService, ISubmissionService submissionService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProblemCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }
            var userId = this.User.Id;

            this.problemService.AddProblem(model.Name, model.Points, userId);

            return this.Redirect("/Home/IndexLoggedIn");
        }

        public IActionResult Details(string id)
        {
            var problem = this.problemService.GetProblemById(id);

            var submissions = this.submissionService.GetSubmissionsByProblemId(id)
                .Select(s => new ProblemDetailViewModel
                {
                    AchievedResult = s.AchievedResult,
                    CreatedOn = s.CreatedOn,
                    MaxPoints = problem.Points,
                    Username = s.User.Username,
                    SubmissionId = s.Id,
                })
                .ToList();

            var model = new ProblemDetailNameViewModel
            {
                Name = problem.Name,
                Submissions = submissions,
            };

            return this.View(model);
        }
    }
}
