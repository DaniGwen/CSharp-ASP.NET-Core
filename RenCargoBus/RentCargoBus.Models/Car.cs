using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RentCargoBus.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Images = new HashSet<CarImage>();
            this.Rents = new HashSet<RentCar>();
            this.IsAvailable = true;
        }

        [Key]
        public int CarId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int Weight { get; set; }

        public double MilesPerGallon { get; set; }

        public int Doors { get; set; }

        public int Seats { get; set; }

        public double HirePrice { get; set; }

        public bool IsAvailable { get; set; }

        public virtual ICollection<CarImage> Images { get; set; }

        public virtual ICollection<RentCar> Rents { get; set; }
    }
}
