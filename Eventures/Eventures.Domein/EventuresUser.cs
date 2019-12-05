using Microsoft.AspNetCore.Identity;

namespace Eventures.Domein
{
    public class EventuresUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UniqueCitizenNumber { get; set; }

        public string Role { get; set; }
    }
}
