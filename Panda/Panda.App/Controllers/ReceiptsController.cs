using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index(string id)
        {
            var receipts = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Include(receipt => receipt.Package)
                .Where(receipt => receipt.RecipientId == id);

            var receiptsListViewModel = new List<PackageReceiptViewModel>();

            foreach (var receipt in receipts)
            {
                var receiptViewModel = new PackageReceiptViewModel
                {
                    Fee = receipt.Fee,
                    Id = receipt.Id,
                    IssuedOn = receipt.IssuedOn,
                    Package = receipt.Package
                };

                receiptsListViewModel.Add(receiptViewModel);
            }

            return View(receiptsListViewModel);
        }
    }
}