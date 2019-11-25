using Microsoft.AspNetCore.Mvc;
using Panda.Data;

namespace Panda.App.Controllers
{
    public class PackageController : Controller
    {
        private readonly PandaDbContext context;

        public PackageController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}