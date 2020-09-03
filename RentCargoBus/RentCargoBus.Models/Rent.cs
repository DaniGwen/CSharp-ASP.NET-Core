using RentCargoBus.Data.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCargoBus.Data.Models
{
    public class Rent
    {
        public Rent()
        {
            this.Cars = new HashSet<RentCar>();
            this.Vans = new HashSet<RentVan>();
        }

        [Key]
        public int RentId { get; set; }

        public RentType Type { get; set; }

        public virtual ICollection<RentCar> Cars { get; set; }

        public virtual ICollection<RentVan> Vans { get; set; }
    }
}
