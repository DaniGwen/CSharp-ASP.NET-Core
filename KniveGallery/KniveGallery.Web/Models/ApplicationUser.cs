using Microsoft.AspNetCore.Identity;

namespace KniveGallery.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public string Facebook { get; set; }
    }
}
