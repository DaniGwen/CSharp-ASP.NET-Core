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
        }

        [Key]
        public int VanId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        public string PlateNumber { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int MaxLoad { get; set; }

        public double HirePrice { get; set; }

        public VanType Type { get; set; }

        public virtual ICollection<VanImage> Images { get; set; }

        public virtual ICollection<RentVan> Rents { get; set; }
    }
}
