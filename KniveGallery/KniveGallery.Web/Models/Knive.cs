using KniveGallery.Web.Models;
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
        public string BladeType { get; set; }

        [Required]
        public string KniveName { get; set; }

        public double Length { get; set; }

        public double EdgeLength { get; set; }

        [Required]
        public string HandleType { get; set; }

        [Required]
        public string BladeMade { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }

        [JsonIgnore]
        public virtual ICollection<KniveImage> Images { get; set; }
    }
}
