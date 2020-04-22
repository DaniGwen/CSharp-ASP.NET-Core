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
    public class SubmissionsController:Controller
    {
        private readonly ISubmissionService submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionCreatViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create");
            }

            this.submissionService.AddSubmission(model.Code, this.User.Id, model.ProblemId);

            return this.Redirect("/Home/IndexLoggedIn");
        }
    }
}
