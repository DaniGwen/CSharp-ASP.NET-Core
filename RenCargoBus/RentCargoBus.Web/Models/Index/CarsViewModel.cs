using RentCargoBus.Data.Models;
using System.Collections.Generic;

namespace RentCargoBus.Web.Models.Index
{
    public class CarsViewModel
    {
        public CarsViewModel()
        {
            this.Images = new List<CarImage>();
        }
        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int MilesPerGallon { get; set; }

        public int Door { get; set; }

        public int Seats { get; set; }

        public decimal Deposit { get; set; }

        public decimal DepositEu { get; set; }

        public decimal DeliveryBg { get; set; }

        public decimal DeliveryEu { get; set; }

        public decimal HirePrice { get; set; }

        public decimal HirePriceMonth { get; set; }

        public string PlateNumber { get; set; }

        public bool IsAvailable { get; set; }

        public List<CarImage> Images { get; set; }
    }
}
