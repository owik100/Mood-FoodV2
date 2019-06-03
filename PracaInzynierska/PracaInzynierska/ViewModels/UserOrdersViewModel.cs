using PracaInzynierska.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.ViewModels
{
    public class UserOrdersViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public List<Order> Orders { get; set; }
    }
}
