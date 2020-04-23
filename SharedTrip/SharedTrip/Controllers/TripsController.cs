using SharedTrip.Services;
using SharedTrip.ViewModels.TripViewModels;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System.Linq;

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
            var trips = this.tripService.GetTrips()
                .Select(trip => new TripsAllViewModel
                {
                    DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    EndPoint = trip.EndPoint,
                    Seats = trip.Seats,
                    StartPoint = trip.StartPoint,
                    Id = trip.Id,
                }).ToList();

            return this.View(trips);
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

        [Authorize]
        public IActionResult Details(string tripId)
        {
            var trip = this.tripService.GetTripById(tripId);

            var model = new TripDetailsViewModel
            {
                StartPoint = trip.StartPoint,
                Seats = trip.Seats,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Description = trip.Description,
                EndPoint = trip.EndPoint,
            };

            return this.View(model);
        }
    }
}
