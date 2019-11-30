using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.App.Models.Package;
using Panda.Data;
using Panda.Domein;
using System.Linq;

namespace Panda.App.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PandaDbContext context;

        public PackagesController(PandaDbContext context)
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
            Package package = new Package
            {
                Description = bindingModel.Description,
                Recipient = this.context.Users.SingleOrDefault(u => u.UserName == bindingModel.Recipient),
                ShippingAddress = bindingModel.ShippingAddress,
                Weight = bindingModel.Weight,
                Status = this.context.PackageStatus.SingleOrDefault(status => status.Name == "Pending")
            };
            
             this.context.Packages.Add(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Pending");
        }

        [HttpGet]
        public IActionResult Pending()
        { 
            var pendingPackage = new Package
            return this.View();
        }
    }
}