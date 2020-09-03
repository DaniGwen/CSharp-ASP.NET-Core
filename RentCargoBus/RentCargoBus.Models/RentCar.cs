using System.ComponentModel.DataAnnotations.Schema;

namespace RentCargoBus.Data.Models
{
    public class RentCar
    {
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey(nameof(Rent))]
        public int RentId { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
