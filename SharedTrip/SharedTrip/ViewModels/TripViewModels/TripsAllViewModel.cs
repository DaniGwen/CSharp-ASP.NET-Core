using System;

namespace SharedTrip.ViewModels.TripViewModels
{
    public class TripsAllViewModel
    {
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
