using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.App.Models.Receipt
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public string IssuedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public double Weight { get; set; }

        public string PackageDescription { get; set; }

        public string Recipient { get; set; }

        public decimal Fee { get; set; }
    }
}
