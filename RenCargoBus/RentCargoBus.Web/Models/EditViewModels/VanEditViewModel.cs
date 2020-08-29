using RentCargoBus.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentCargoBus.Web.Models.EditViewModels
{
    public class VanEditViewModel
    {
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

        [DisplayName("Consumption")]
        public double KilometerPerLiter { get; set; }

        [Display(Name = "Warranty Deposit for Bulgaria")]
        public decimal Deposit { get; set; }

        [Display(Name = "Warranty Deposit for Europe")]
        public decimal DepositEu { get; set; }

        [Required]
        [DisplayName("Price per Day")]
        public decimal HirePrice { get; set; }

        [Required]
        [DisplayName("Price per Month")]
        public decimal HirePriceMonth { get; set; }

        [DisplayName("Van type")]
        public int Type { get; set; }

        [DisplayName("Is available?")]
        public bool IsAvailable { get; set; }

        public List<VanImage> Images{ get; set; }
    }
}
