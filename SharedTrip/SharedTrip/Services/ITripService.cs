using SharedTrip.Models;
using SharedTrip.ViewModels.TripViewModels;
using System.Linq;

namespace SharedTrip.Services
{
    public interface ITripService
    {
        void AddTrip(CreateTripViewModel model);

        IQueryable<Trip> GetTrips();

        Trip GetTripById(string id);

        void CreateUserTrip(string tripDbId, string userId);

        bool IsUserJoined(string userId, string tripId);
    }
}
