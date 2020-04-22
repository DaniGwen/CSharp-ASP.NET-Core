using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IProblemService problemService;

        public SubmissionsController(ISubmissionService submissionService, IProblemService problemService)
        {
            this.submissionService = submissionService;
            this.problemService = problemService;
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            var problem = this.problemService.GetProblemById(id);

            var model = new SubmissionGetCreateViewModel
            {
                Name = problem.Name,
                ProblemId = problem.Id,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionCreatViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect($"/Submissions/Create?id={model.ProblemId}");
            }

            this.submissionService.AddSubmission(model.Code, this.User.Id, model.ProblemId);

            return this.Redirect("/Home/IndexLoggedIn");
        }
    }
}
