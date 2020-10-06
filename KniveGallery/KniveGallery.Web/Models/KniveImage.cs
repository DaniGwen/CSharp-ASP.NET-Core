using KniveGallery.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KniveGallery.Web.Models
{
    public class KniveImage
    {
        [Key]
        public int ImageId { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey(nameof(Knive))]
        public int KniveId { get; set; }

        public virtual Knive Knive { get; set; }
    }
}
