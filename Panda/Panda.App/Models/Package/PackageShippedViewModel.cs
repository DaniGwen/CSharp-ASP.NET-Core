namespace Panda.App.Models.Package
{
    public class PackageShippedViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Recipient { get; set; }

        public double Weight { get; set; }

        public string DeliveryDate { get; set; }
    }
}
