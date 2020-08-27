using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;

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

        public string Brand { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int Seats { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int MaxLoad { get; set; }

        public double KilometerPerLiter { get; set; }

        public double HirePrice { get; set; }

        public int Type { get; set; }

        public int IsAvailable { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<int> Image { get; set; }
    }
}
