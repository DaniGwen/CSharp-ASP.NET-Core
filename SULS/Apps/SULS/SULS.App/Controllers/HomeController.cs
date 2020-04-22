using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Services;
using System.Collections.Generic;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        // /
        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        // /Home/Index
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            var model = this.problemService.GetProblemsByUserId(this.User.Id).Select(x => new ProblemViewModel
            {
                Name = x.Name,
                Count = x.Submissions.Count,
                Id = x.Id,
            });
  
            return this.View(model);
        }
    }
}