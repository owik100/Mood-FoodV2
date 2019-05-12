using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
