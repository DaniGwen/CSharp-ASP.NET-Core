using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.Package;
using Panda.Data;
using Panda.Domein;
using System;
using System.Collections.Generic;
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
                Id = Guid.NewGuid().ToString(),
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
            var packagesDb = this.context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Pending")
                .ToList();

            var viewModelPackages = new List<PackagePendingViewModel>();

            foreach (var package in packagesDb)
            {
                var packageForViewModel = new PackagePendingViewModel
                {
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    ShippingAddress = package.ShippingAddress,
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageForViewModel);
            }

            return this.View(viewModelPackages);
        }

        [HttpGet]
        public IActionResult Shipped()
        {
            var packagesDb = this.context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Shipped")
                .ToList();

            var viewModelPackages = new List<PackageShippedViewModel>();

            foreach (var package in packagesDb)
            {
                var packageForViewModel = new PackageShippedViewModel
                {
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    DeliveryDate = DateTime.UtcNow.AddDays(20).ToString("dd/m/yyyy"),
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageForViewModel);
            }

            return this.View(viewModelPackages);
        }

        [HttpGet]
        public IActionResult Delivered()
        {
            var packagesDb = this.context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Delivered")
                .ToList();

            var viewModelPackages = new List<PackageDeliveredViewModel>();

            foreach (var package in packagesDb)
            {
                var packageForViewModel = new PackageDeliveredViewModel
                {
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    ShippingAddress = package.ShippingAddress,
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageForViewModel);
            }

            return this.View(viewModelPackages);
        }
    }
}