﻿using RentCargoBus.Data.Models;
using RentCargoBus.Data.Models.Enum;
using System.Collections.Generic;

namespace RentCargoBus.Web.Models.Index
{
    public class VansViewModel
    {
        public VansViewModel()
        {
            this.Images = new List<VanImage>();
        }

        public int VanId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Seats { get; set; }

        public int MaxLoad { get; set; }

        public decimal DepositBg { get; set; }

        public decimal DepositEu { get; set; }

        public decimal DeliveryBg { get; set; }

        public decimal DeliveryEu { get; set; }

        public decimal HirePrice { get; set; }

        public decimal HirePriceMonth { get; set; }

        public double KilometersPerLiter { get; set; }

        public string PlateNumber { get; set; }

        public bool IsAvailable { get; set; }

        public VanType Type { get; set; }

        public List<VanImage> Images { get; set; }
    }
}
