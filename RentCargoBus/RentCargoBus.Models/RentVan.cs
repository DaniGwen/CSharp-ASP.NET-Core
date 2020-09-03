using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCargoBus.Data.Models
{
    public class RentVan
    {
        [ForeignKey(nameof(Van))]
        public int VanId { get; set; }

        public virtual Van Van { get; set; }

        [ForeignKey(nameof(Rent))]
        public int RentId { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
