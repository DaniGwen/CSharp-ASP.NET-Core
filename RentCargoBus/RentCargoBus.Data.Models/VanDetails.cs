using System;

namespace RentCargoBus.Data.Models
{
    public class VanDetails
    {
        public int BusId { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string PlateNumber { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int MaxLoad { get; set; }

        public decimal HirePrice { get; set; }
    }
}
