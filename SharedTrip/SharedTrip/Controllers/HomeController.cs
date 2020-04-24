namespace SharedTrip.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Result;

    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public IActionResult SlashIndex()
        {
            if (this.IsLoggedIn())
            {
                return this.Redirect("Trips/All");
            }

            return this.Index();
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}