using RentCargoBus.Data.Models.Enum;

namespace RentCargoBus.Web.Models.Index
{
    public class VansViewModel
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Seats { get; set; }

        public int MaxLoad { get; set; }

        public decimal HirePrice { get; set; }

        public VanType Type { get; set; }
    }
}
