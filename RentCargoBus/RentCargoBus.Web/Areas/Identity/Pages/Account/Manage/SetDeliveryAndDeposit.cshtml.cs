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
        private readonly IDeliveryAndDepositService deliveryAndDepositService;
        private readonly IMapper mapper;

        public EditDeliveryModel(IDeliveryAndDepositService deliveryAndDepositService, IMapper mapper)
        {
            this.deliveryAndDepositService = deliveryAndDepositService;
            this.mapper = mapper;
        }

        [ViewData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Car delivery price for BG")]
            public decimal CarDeliveryBg { get; set; }

            [DisplayName("Car delivery price for EU")]
            public decimal CarDeliveryEu { get; set; }

            [DisplayName("Van delivery price for BG")]
            public decimal VanDeliveryBg { get; set; }

            [DisplayName("Van delivery price for EU")]
            public decimal VanDeliveryEu { get; set; }

            [DisplayName("Van deposit price for EU")]
            public decimal VanDepositEu { get; set; }

            [DisplayName("Van deposit price for BG")]
            public decimal VanDepositBg { get; set; }

            [DisplayName("Car deposit price for BG")]
            public decimal CarDepositBg { get; set; }

            [DisplayName("Car deposit price for EU")]
            public decimal CarDepositEu { get; set; }
        }

        public IActionResult OnGet()
        {
            var deliveryAndDepositsDb = this.deliveryAndDepositService.GetDeliveryAndDeposits();

            this.Input = this.mapper.Map(deliveryAndDepositsDb, this.Input);

            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Error. Check all fields and try again.";
                return this.Page();
            }

            var deliveryDeposit = this.mapper.Map<DeliveryAndDeposit>(Input);

            this.deliveryAndDepositService.SetDeliveryFees(deliveryDeposit);

            StatusMessage = "Successfuly saved!";
            return this.Page();
        }
    }
}
