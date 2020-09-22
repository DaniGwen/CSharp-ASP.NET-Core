namespace KniveGallery.Data.Models
{
    public class Knive
    {
        public int KniveId { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public double EdgeLength { get; set; }

        public string HandleType { get; set; }

        public string BladeType { get; set; }

        public double Price { get; set; }
    }
}
