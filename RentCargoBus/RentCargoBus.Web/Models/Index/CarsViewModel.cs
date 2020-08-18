using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCargoBus.Web.Models.Index
{
    public class CarsViewModel
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        public int MilesPerGallon { get; set; }

        public int Door { get; set; }

        public int Seats { get; set; }

        public decimal HirePrice { get; set; }
    }
}
