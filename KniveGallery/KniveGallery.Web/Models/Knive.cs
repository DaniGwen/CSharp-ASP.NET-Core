using KniveGallery.Web.Models;
using KniveGallery.Web.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KniveGallery.Web.Data
{
    public class Knive
    {
        public Knive()
        {
            this.Images = new HashSet<KniveImage>();
        }

        [Key]
        public int KniveId { get; set; }

        [Required]
        public string EdgeMade { get; set; }

        public double TotalLength { get; set; }

        public double EdgeLength { get; set; }

        public double? EdgeWidth { get; set; }

        public double? EdgeTickness { get; set; }

        [Required]
        public string HandleDescription { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }

        public int Quantity { get; set; }

        public KniveClass KniveClass { get; set; }

        [JsonIgnore]
        public virtual ICollection<KniveImage> Images { get; set; }
    }
}
