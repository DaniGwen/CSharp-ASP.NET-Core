using RentCargoBus.Data.Models;

namespace RentCargoBus.Services.Contracts
{
    public interface IDeliveryAndDepositService
    {
        void SetDeliveryFees(DeliveryAndDeposit delivery);

        DeliveryAndDeposit GetDeliveryAndDeposits();
    }
}
