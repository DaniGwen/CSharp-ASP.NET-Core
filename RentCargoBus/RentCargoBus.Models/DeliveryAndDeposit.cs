namespace RentCargoBus.Data.Models
{
    public class DeliveryAndDeposit
    {
        public int Id { get; set; }

        public decimal CarDeliveryBg { get; set; }

        public decimal CarDeliveryEu { get; set; }

        public decimal VanDeliveryBg { get; set; }

        public decimal VanDeliveryEu { get; set; }

        public decimal CarDepositBg { get; set; }

        public decimal CarDepositEu { get; set; }

        public decimal VanDepositBg { get; set; }

        public decimal VanDepositEu { get; set; }
    }
}
