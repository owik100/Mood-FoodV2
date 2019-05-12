using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfImage { get; set; }
        public decimal Price { get; set; }
        public bool Hidden { get; set; }

        public Category Category { get; set; }
    }
}
