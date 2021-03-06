﻿using System;

namespace SharedTrip.ViewModels.TripViewModels
{
    public class TripsAllViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
