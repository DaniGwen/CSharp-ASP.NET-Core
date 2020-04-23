using SharedTrip.Services;
using SharedTrip.ViewModels.UserViewModel;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = this.userService.GetUserOrNull(model.Username, model.Password);

            if (user == null)
            {
                return this.View();
            }

            this.SignIn(user.Id, user.Username, user.Email);

            return this.Redirect("/Trips/All");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(CreateUserViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Users/Register");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            this.userService.CreateUser(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }
    }
}
