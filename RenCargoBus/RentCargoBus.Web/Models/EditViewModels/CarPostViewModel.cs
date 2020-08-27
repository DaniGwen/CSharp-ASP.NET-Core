using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCargoBus.Web.Models.EditViewModels
{
    public class CarPostViewModel
    {
        public CarPostViewModel()
        {
            this.Images = new List<IFormFile>();
            this.ImagesToDelete = new List<int>();
        }

        public int CarId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        public int Weight { get; set; }

        public double MilesPerGallon { get; set; }

        public int Doors { get; set; }

        public int Seats { get; set; }

        public double HirePrice { get; set; }

        public int IsAvailable { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<int> ImagesToDelete { get; set; }
    }
}
