using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        public TripsController(ITripService tripService)
        {

        }

        [Authorize]
        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add()
        {
            return this.View();
        }
    }
}
