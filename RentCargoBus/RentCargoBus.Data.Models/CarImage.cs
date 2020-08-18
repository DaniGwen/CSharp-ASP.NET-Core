using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCargoBus.Data.Models
{
    public class CarImage
    {
        [Key]
        public int ImageId { get; set; }

        public string Title { get; set; }

        public string ImageLink { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
