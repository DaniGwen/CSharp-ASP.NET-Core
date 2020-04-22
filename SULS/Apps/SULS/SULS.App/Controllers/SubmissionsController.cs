using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Controllers
{
    public class SubmissionsController:Controller
    {
        public SubmissionsController()
        {

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
            return this.View();
        }
    }
}
