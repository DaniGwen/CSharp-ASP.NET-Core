using KniveGallery.Web.Data;

namespace KniveGallery.Web.Models
{
    public class OrderedKniveIds
    {
        public int Id { get; set; }

        public int KniveId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
