﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        [Display (Name ="Ilość")]
        public int Quantity { get; set; }
        [Display(Name = "Cena")]
        public decimal PurchasePrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
