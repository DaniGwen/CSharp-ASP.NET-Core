using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.Models.BindingModels
{
    public class EventCreateBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Total tickets")]
        [Range(0, int.MaxValue , ErrorMessage = "Number must be positive")]
        public int TotalTickets { get; set; }

        [Required]
        [Display(Name = "Price per ticket")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335M", ErrorMessage = "Price per ticket must be positive number")]
        public decimal PricePerTicket { get; set; }
    }
}
