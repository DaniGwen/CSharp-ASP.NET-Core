using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.Package;
using Panda.Data;
using Panda.Domein;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                var packageViewModel = new PackagePendingViewModel
                {
                    Id = package.Id,
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    ShippingAddress = package.ShippingAddress,
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageViewModel);
            }

            return this.View(viewModelPackages);
        }

        [HttpGet]
        public IActionResult Ship(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.EstimatedDeliveryDate = DateTime.UtcNow
                                                    .AddDays(new Random().Next(20, 40));
            package.Status = this.context.PackageStatus
                                         .SingleOrDefault(status => status.Name == "Shipped");
            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Shipped");
        }

        [HttpGet]
        public IActionResult Deliver(string id)
        {
            Package package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatus
                                         .SingleOrDefault(status => status.Name == "Delivered");
            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Delivered");
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
                var packageViewModel = new PackageShippedViewModel
                {
                    Id = package.Id,
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    DeliveryDate = package.EstimatedDeliveryDate?
                                          .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageViewModel);
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
                var packageViewModel = new PackageDeliveredViewModel
                {
                    Id = package.Id,
                    Description = package.Description,
                    Recipient = package.Recipient.UserName,
                    ShippingAddress = package.ShippingAddress,
                    Weight = package.Weight
                };

                viewModelPackages.Add(packageViewModel);
            }

            return this.View(viewModelPackages);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var packageDB = this.context.Packages
                .Include(package => package.Recipient)
                .Include(package => package.Status)
                .SingleOrDefault(x=> x.Id == id);

            var viewModelPackage = new PackageDetailsViewModel
            {
                Id = packageDB.Id,
                Address = packageDB.ShippingAddress,
                Description = packageDB.Description,
                EstimatedDeliveryDate = packageDB.EstimatedDeliveryDate?.ToString("dd/MM/yyyy"),
                Recipient = packageDB.Recipient.UserName,
                Status = packageDB.Status.Name,
                Weight = packageDB.Weight

            };

            return this.View(viewModelPackage); 
        }
    }
}