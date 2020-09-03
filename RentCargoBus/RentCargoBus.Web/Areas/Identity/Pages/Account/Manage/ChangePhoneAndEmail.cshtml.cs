using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentCargoBus.Data.Models;
using RentCargoBus.Services.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RentAVan.Web.Areas.Identity.Pages.Account.Manage
{
    public class ChangePhoneAndEmailModel : PageModel
    {
        private readonly IGeneralService generalService;

        public ChangePhoneAndEmailModel(IGeneralService generalService)
        {
            this.generalService = generalService;
        }

        [ViewData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please add email")]
            [DisplayName("Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please add phone number")]
            [DisplayName("Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGet()
        {
            var emailAndPhoneDb = await this.generalService.GetPhoneEmail();

            if (emailAndPhoneDb != null)
            {
                this.Input = new InputModel
                {
                    Email = emailAndPhoneDb.Email,
                    PhoneNumber = emailAndPhoneDb.PhoneNumber
                };
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                this.StatusMessage = "Error. Complete all fields and try again.";

                return this.Page();
            }

            var phoneEmail = new PhoneEmail
            {
                Email = this.Input.Email,
                PhoneNumber = this.Input.PhoneNumber,
            };

            this.generalService.SetPhoneEmail(phoneEmail);

            this.StatusMessage = "Successfuly changed email and phone.";

            return this.Page();
        }
    }
}
