using SharedTrip.ViewModels.TripViewModels;

namespace SharedTrip.Services
{
    public interface ITripService
    {
        void AddTrip(CreateTripViewModel model);
    }
}
