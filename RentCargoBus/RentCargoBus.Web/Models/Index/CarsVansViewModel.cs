using RentCargoBus.Data.Models;
using System.Collections.Generic;

namespace RentCargoBus.Web.Models.Index
{
    public class CarsVansViewModel
    {
        public CarsVansViewModel()
        {
            this.Vans = new List<VansViewModel>();
            this.Cars = new List<CarsViewModel>();
        }
        public List<VansViewModel> Vans { get; set; }

        public List<CarsViewModel> Cars { get; set; }

        public DeliveryAndDeposit DeliveryFees { get; set; }
    }
}
