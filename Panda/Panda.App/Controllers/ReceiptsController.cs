using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.Receipt;
using Panda.Data;

namespace Panda.App.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext context;

        public ReceiptsController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize]  
        public IActionResult Index(string id)
        {
            var receiptsViewModel = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Include(receipt => receipt.Package)
                .Where(receipt => receipt.Recipient.UserName == this.User.Identity.Name)
                .Select(receipt => new ReceiptIndexViewModel
                {
                    Fee = receipt.Fee,
                    Id = receipt.Id,
                    IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Recipient = receipt.Recipient.UserName
                })
                .ToList();

            return View(receiptsViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var receipt = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Include(receipt => receipt.Package)
                .Select(receipt => new ReceiptDetailsViewModel
                {
                    DeliveryAddress = receipt.Package.ShippingAddress,
                    Id = receipt.Id,
                    Weight = receipt.Package.Weight,
                    IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy"),
                    PackageDescription = receipt.Package.Description,
                    Recipient = receipt.Recipient.UserName,
                    Fee = receipt.Fee
                })
                .SingleOrDefault(receipt => receipt.Id == id);

            return this.View(receipt);
        }
    }
}