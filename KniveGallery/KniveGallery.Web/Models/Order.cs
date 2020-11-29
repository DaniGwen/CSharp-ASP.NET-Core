using System;

namespace KniveGallery.Web.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int KniveId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string City { get; set; }
        public string Neighbourhood { get; set; }
        public string Street { get; set; }
        public bool IsDelivered { get; set; }
        public string OrderDate { get; set; }
        public string DispatchDate { get; set; }
    }
}
