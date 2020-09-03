using Microsoft.AspNetCore.Identity;

namespace RentCargoBus.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PersonName { get; set; }
    }
}
