using Panda.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.App.Models.Package
{
    public class PackageCreateBindingModel
    {
        public string Description { get; set; }
         
        public string ShippingAddress { get; set; }

        public double Weight { get; set; }

        public string Recipient { get; set; }


    }
}
