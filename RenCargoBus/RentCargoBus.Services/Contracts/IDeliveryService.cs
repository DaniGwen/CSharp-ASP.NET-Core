using RentCargoBus.Data.Models;

namespace RentCargoBus.Services.Contracts
{
    public interface IDeliveryService
    {
        void SetDeliveryFees(Delivery delivery);

        Delivery GetDeliveryFees();
    }
}
