using System;

namespace Panda.App.Models.Package
{
    public class PackageReceiptViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal Fee { get; set; }

        public Panda.Domein.Package Package { get; set; }
    }
}
