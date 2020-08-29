using RentCargoBus.Data.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCargoBus.Data.Models
{
    public class Van
    {
        public Van()
        {
            this.Images = new HashSet<VanImage>();
            this.Rents = new HashSet<RentVan>();
            this.IsAvailable = true;
        }

        [Key]
        public int VanId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int Seats { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int MaxLoad { get; set; }

        public decimal Deposit { get; set; }

        public decimal DepositEu { get; set; }

        public decimal HirePrice { get; set; }

        public decimal HirePriceMonth { get; set; }

        public double KilometersPerLiter { get; set; }

        public VanType Type { get; set; }

        public bool IsAvailable { get; set; }

        public virtual ICollection<VanImage> Images { get; set; }

        public virtual ICollection<RentVan> Rents { get; set; }
    }
}
