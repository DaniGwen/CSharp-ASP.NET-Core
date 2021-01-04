using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentCargoBus.Web.Models.Index
{
    public class CarsViewModel
    {
        public CarsViewModel()
        {
            this.Images = new List<CarImage>();
        }
        private string model;
        private string brand;
        private string plateNumber;

        public int CarId { get; set; }

        public string Brand
        {
            get { return brand; }
            set { brand = value.First().ToString().ToUpper() + value.Substring(1).ToLower(); }
        }

        public string Model
        {
            get { return model; }
            set { model = value.First().ToString().ToUpper() + value.Substring(1).ToLower(); }
        }

        public int MilesPerGallon { get; set; }

        public int Doors { get; set; }

        public int Seats { get; set; }

        public decimal DepositBg { get; set; }

        public decimal DepositEu { get; set; }

        public decimal DeliveryBg { get; set; }

        public decimal DeliveryEu { get; set; }

        public decimal HirePrice { get; set; }

        public decimal HirePriceMonth { get; set; }

        public string PlateNumber
        {
            get { return plateNumber; }
            set { plateNumber = value.ToUpper(); }
        }

        public bool IsAvailable { get; set; }

        public List<CarImage> Images { get; set; }
    }
}
