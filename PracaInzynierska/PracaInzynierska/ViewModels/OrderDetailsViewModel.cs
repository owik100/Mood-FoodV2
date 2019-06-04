using PracaInzynierska.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.ViewModels
{
    public class OrderDetailsViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public Order Order { get; set; }
    }
}
