using Microsoft.AspNetCore.Http;
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("Consumption")]
        public double MilesPerGallon { get; set; }

        public int Doors { get; set; }

        [DisplayName("Warranty Deposit for Bulgaria")]
        public decimal Deposit { get; set; }

        [DisplayName("Warranty Deposit for Europe")]
        public decimal DepositEu { get; set; }

        [DisplayName("Price per Day")]
        public decimal HirePrice { get; set; }

        [DisplayName("Price per Month")]
        public decimal HirePriceMonth { get; set; }

        [DisplayName("Is avilable?")]
        public int IsAvailable { get; set; }

        public List<CarImage> Images { get; set; }
    }
}
