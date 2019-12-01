using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.Package;
using Panda.Data;
using System.Linq;

namespace Panda.App.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly PandaDbContext context;

        public HomeController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userPackages = this.context.Packages
                    .Where(package => package.Recipient.UserName == this.User.Identity.Name)
                    .Include(package => package.Status)
                    .Select(package => new PackageHomeViewModel
                    {
                        Description = package.Description,
                        Status = package.Status.Name,
                        Id = package.Id
                    })
                    .ToList();

                return this.View(userPackages);
            }

            return this.View();
        }
    }
}
