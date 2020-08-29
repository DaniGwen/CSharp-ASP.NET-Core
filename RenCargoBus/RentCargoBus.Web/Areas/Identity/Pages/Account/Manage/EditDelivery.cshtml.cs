using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentCargoBus.Services.Contracts;

namespace RentAVan.Web.Areas.Identity.Pages.Account.Manage
{
    public class EditDeliveryModel : PageModel
    {
        private readonly IDeliveryService deliveryService;

        public EditDeliveryModel(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Car delivery price")]
            public decimal CarDelivery { get; set; }

            [DisplayName("Van delivery price")]
            public decimal VanDelivery { get; set; }
        }

        public IActionResult OnGet()
        { 
            return this.Page();
        }

        public IActionResult OnPost()
        {

            this.deliveryService.SetDeliveryFees(Input.CarDelivery, Input.VanDelivery);

            return this.Redirect("/Identity/Account/Manage");
        }
    }
}
