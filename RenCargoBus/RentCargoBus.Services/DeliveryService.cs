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
    public class DeliveryAndDepositService : IDeliveryAndDepositService
    {
        private readonly ApplicationDbContext context;

        public DeliveryAndDepositService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SetDeliveryFees(DeliveryAndDeposit delivery)
        {
            var deliveryDb = this.context.DeliveryAndDeposit.FirstOrDefault();

            if (deliveryDb == null)
            {
                this.context.DeliveryAndDeposit.Add(delivery);
            }
            else
            {
                deliveryDb.VanDeliveryBg = delivery.VanDeliveryBg;
                deliveryDb.VanDeliveryEu = delivery.VanDeliveryEu;
                deliveryDb.VanDepositBg = delivery.VanDepositBg;
                deliveryDb.VanDepositEu = delivery.VanDepositEu;

                deliveryDb.CarDepositBg = delivery.CarDepositBg;
                deliveryDb.CarDepositEu = delivery.CarDepositEu;
                deliveryDb.CarDeliveryBg = delivery.CarDeliveryBg;
                deliveryDb.CarDeliveryEu = delivery.CarDeliveryEu;
            }

            this.context.SaveChanges();
        }

        public DeliveryAndDeposit GetDeliveryAndDeposits()
        {
            return this.context.DeliveryAndDeposit.FirstOrDefault();
        }
    }
}
