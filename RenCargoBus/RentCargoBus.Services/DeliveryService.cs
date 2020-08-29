using Microsoft.EntityFrameworkCore.Internal;
using RentCargoBus.Data;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RentCargoBus.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ApplicationDbContext context;

        public DeliveryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SetDeliveryFees(decimal carPrice, decimal vanPrice)
        {
            var deliveryFees = this.context.VehiclesDelivery.FirstOrDefault();

            if (deliveryFees == null)
            {
                this.context.VehiclesDelivery.Add(new Delivery
                {
                    CarDelivery = carPrice,
                    VanDelivery = vanPrice
                });
            }
            else
            {
                deliveryFees.VanDelivery = vanPrice;
                deliveryFees.CarDelivery = carPrice;
            }

            this.context.SaveChanges();
        }

        public Delivery GetDeliveryFees()
        {
            return this.context.VehiclesDelivery.FirstOrDefault();
        }
    }
}
