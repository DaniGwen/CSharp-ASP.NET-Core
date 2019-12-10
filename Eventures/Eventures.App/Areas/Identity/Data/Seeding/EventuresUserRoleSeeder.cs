using System.Linq;
using Eventures.App.Data;
using Microsoft.AspNetCore.Identity;

namespace Eventures.App.Areas.Identity.Data.Seeding
{
    public class EventuresUserRoleSeeder
    {
        public void Seed(EventuresDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });

                context.SaveChanges();
            }
        }
    }
}
