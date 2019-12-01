using System;

namespace Panda.App.Models.Receipt
{
    public class PackageReceiptViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal Fee { get; set; }

        public Domein.Package Package { get; set; }
    }
}
