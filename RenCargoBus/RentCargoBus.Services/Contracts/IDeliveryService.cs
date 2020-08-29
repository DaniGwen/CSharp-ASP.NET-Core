using RentCargoBus.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentCargoBus.Services.Contracts
{
    public interface IDeliveryService
    {
        void SetDeliveryFees(decimal carPrice, decimal vanPrice);

        Delivery GetDeliveryFees();
    }
}
