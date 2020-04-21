using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
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
            //TODO Get problems from DB
            var model = new List<ProblemViewModel>();
            return this.View(model);
        }
    }
}