using Microsoft.AspNetCore.Http;
using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentCargoBus.Web.Models.EditViewModels
{
    public class VanEditPostModel
    {
        public VanEditPostModel()
        {
            this.Image = new List<int>();
            this.Images = new List<IFormFile>();
        }

        public int VanId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [DisplayName("Plate number")]
        public string PlateNumber { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        [DisplayName("Max load")]
        public int MaxLoad { get; set; }

        [Required]
        [DisplayName("Price per day")]
        public double HirePrice { get; set; }

        [DisplayName("Van type")]
        public int Type { get; set; }

        [DisplayName("Is available?")]
        public bool IsAvailable { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<int> Image { get; set; }
    }
}
