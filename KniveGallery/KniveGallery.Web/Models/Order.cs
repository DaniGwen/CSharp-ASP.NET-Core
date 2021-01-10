using AutoMapper.Configuration.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KniveGallery.Web.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderedKniveIds = new List<OrderedKniveIds>();
            this.KniveIds = new List<int>();
        }
        public int OrderId { get; set; }
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

        [NotMapped]
        public List<int> KniveIds { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderedKniveIds> OrderedKniveIds { get; set; }
    }
}
