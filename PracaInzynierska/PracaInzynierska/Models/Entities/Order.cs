using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZIPCode { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Emial { get; set; }
        public string OptionalDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderValue { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Complited
    }
}
}
