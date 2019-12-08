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
        public int TotalTickets { get; set; }


        [Required]
        [Display(Name = "Price per ticket")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price per ticket must be positive number")]
        public decimal PricePerTicket { get; set; }
    }
}
