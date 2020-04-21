using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Users;

namespace SULS.App.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            this.SignIn()
            return this.View();
        }
    }
}