namespace KnifeGallery.Data.Models
{
    public class Knife
    {
        public int KnifeId { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public int EdgeLength { get; set; }

        public string HandleType { get; set; }

        public string BladeType { get; set; }

        public double Price { get; set; }
    }
}
