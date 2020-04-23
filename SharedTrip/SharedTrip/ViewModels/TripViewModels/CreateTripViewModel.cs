using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.ViewModels.TripViewModels
{
    public class CreateTripViewModel
    {
        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        public string ImagePath { get; set; }

        [Required]
        [RangeSis(2, 6, "error")]
        public int Seats { get; set; }

        [Required]
        [StringLengthSis(1, 80, "error")]
        public string Description { get; set; }
    }
}
