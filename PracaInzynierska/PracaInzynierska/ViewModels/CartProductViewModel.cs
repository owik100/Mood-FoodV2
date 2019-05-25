using PracaInzynierska.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.ViewModels
{
    public class CartProductViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }

    }
}
