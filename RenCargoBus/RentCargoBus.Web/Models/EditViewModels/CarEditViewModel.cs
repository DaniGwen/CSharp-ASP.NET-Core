using Microsoft.AspNetCore.Http;
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace RentCargoBus.Web.Models.EditViewModels
{
    public class CarEditViewModel
    {
        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        [DisplayName("Plate number")]
        public string PlateNumber { get; set; }

        public int Weight { get; set; }

        [DisplayName("Kilometers per liter")]
        public double MilesPerGallon { get; set; }

        public int Doors { get; set; }

        public int Seats { get; set; }

        [DisplayName("Price per day")]
        public double HirePrice { get; set; }

        [DisplayName("Is avilable?")]
        public int IsAvailable { get; set; }

        public List<CarImage> Images { get; set; }
    }
}
