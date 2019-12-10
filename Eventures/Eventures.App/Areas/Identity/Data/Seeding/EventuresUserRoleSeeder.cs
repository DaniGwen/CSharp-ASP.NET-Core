using System.Linq;
using Eventures.App.Data;
using Microsoft.AspNetCore.Identity;

namespace Eventures.App.Areas.Identity.Data.Seeding
{
    public class EventuresUserRoleSeeder : ISeeder
    {
        private readonly EventuresDbContext context;

        public EventuresUserRoleSeeder(EventuresDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (!this.context.Users.Any())
            {
                this.context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                this.context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });

                this.context.SaveChanges();
            }
        }
    }
}
