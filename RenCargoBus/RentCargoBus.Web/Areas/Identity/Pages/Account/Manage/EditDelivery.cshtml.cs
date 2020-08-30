using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;

namespace RentAVan.Web.Areas.Identity.Pages.Account.Manage
{
    public class EditDeliveryModel : PageModel
    {
        private readonly IDeliveryService deliveryService;
        private readonly IMapper mapper;

        public EditDeliveryModel(IDeliveryService deliveryService, IMapper mapper)
        {
            this.deliveryService = deliveryService;
            this.mapper = mapper;
        }

        [ViewData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Car delivery price \"BG\"")]
            public decimal CarDeliveryBg { get; set; }

            [DisplayName("Car delivery price \"EU\"")]
            public decimal CarDeliveryEu { get; set; }

            [DisplayName("Van delivery price \"BG\"")]
            public decimal VanDeliveryBg { get; set; }

            [DisplayName("Van delivery price \"EU\"")]
            public decimal VanDeliveryEu { get; set; }
        }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Error. Check all fields and try again.";
                return this.Page();
            }

            var delivery = this.mapper.Map<Delivery>(Input);

            this.deliveryService.SetDeliveryFees(delivery);

            StatusMessage = "Successfuly saved!";
            return this.Page();
        }
    }
}
