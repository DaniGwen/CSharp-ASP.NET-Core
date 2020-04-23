using SharedTrip.Services;
using SharedTrip.ViewModels.TripViewModels;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
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
        public IActionResult Add(CreateTripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Trips/Add");
            }

            this.tripService.AddTrip(model);

            return this.Redirect("/Trips/All");
        }
    }
}
