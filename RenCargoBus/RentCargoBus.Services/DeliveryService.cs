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

        public void SetDeliveryFees(Delivery delivery)
        {
            var deliveryDb = this.context.VehiclesDelivery.FirstOrDefault();

            if (deliveryDb == null)
            {
                this.context.VehiclesDelivery.Add(delivery);
            }
            else
            {
                deliveryDb.VanDeliveryBg = delivery.VanDeliveryBg;
                deliveryDb.VanDeliveryEu = delivery.VanDeliveryEu;
                deliveryDb.CarDeliveryBg = delivery.CarDeliveryBg;
                deliveryDb.CarDeliveryEu = delivery.CarDeliveryEu;
            }

            this.context.SaveChanges();
        }

        public Delivery GetDeliveryFees()
        {
            return this.context.VehiclesDelivery.FirstOrDefault();
        }
    }
}
