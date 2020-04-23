using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.TripViewModels
{
    public class CreateTripViewModel
    {
        [RequiredSis]
        public string StartingPoint { get; set; }

        [RequiredSis]
        public string EndPoint { get; set; }

        [RequiredSis]
        public DateTime DepartureTime { get; set; }

        public string ImagePath { get; set; }

        [RequiredSis]
        [RangeSis(2, 6, null)]
        public int Seats { get; set; }

        [RequiredSis]
        public string Description { get; set; }
    }
}
