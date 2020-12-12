using System.ComponentModel.DataAnnotations;

namespace KniveGallery.Web.Models
{
    public class CarouselImage
    {
        [Key]
        public int ImageId { get; set; }

        public string ImagePath { get; set; }
    }
}
