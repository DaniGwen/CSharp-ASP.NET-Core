using Eventures.App.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.Models.BindingModels
{
    public class EventCreateBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        [IsAfterNow(ErrorMessage = "Date and time must be after current moment")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        [IsAfterNow(ErrorMessage = "Date and time must be after current moment")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Total tickets")]
        [Range(1, int.MaxValue, ErrorMessage = "Number must be positive")]
        public int TotalTickets { get; set; }

        [Required]
        [Display(Name = "Price per ticket")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Price per ticket must be positive number")]

        [DataType(nameof(Decimal), ErrorMessage = "Input must be a number")]
        public decimal PricePerTicket { get; set; } 
    }
}
