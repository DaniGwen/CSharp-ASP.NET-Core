using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.App.Models.Package;
using Panda.Data;
using Panda.Domein;
using System.Linq;

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
            this.ViewData["Recipients"] = this.context.Users.ToList();

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(PackageCreateBindingModel bindingModel)
        {
            return View();
        }
    }
}