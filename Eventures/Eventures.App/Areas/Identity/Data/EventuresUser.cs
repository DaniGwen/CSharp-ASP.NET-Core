using Microsoft.AspNetCore.Identity;

namespace Eventures.App.Models
{
    // Add profile data for application users by adding properties to the EventuresUser class
    public class EventuresUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

    }
}
