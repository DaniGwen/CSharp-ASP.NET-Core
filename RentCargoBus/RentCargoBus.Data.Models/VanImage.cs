using RentCargoBus.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCargoBus.Data.Models
{
    public class VanImage
    {
        [Key]
        public int ImageId { get; set; }

        public string Title { get; set; }

        public string ImageLink { get; set; }

        [ForeignKey(nameof(Van))]
        public int VanId { get; set; }

        public virtual Van Van { get; set; }
    }
}
