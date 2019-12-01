using System;

namespace Panda.App.Models.Receipt
{
    public class ReceiptIndexViewModel
    {
        public string Id { get; set; }

        public string IssuedOn { get; set; }

        public decimal Fee { get; set; }

        public string Recipient { get; set; }
    }
}
