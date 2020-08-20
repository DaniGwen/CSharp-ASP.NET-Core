using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentCargoBus.Web.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {

        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
