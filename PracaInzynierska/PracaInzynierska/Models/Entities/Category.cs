using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfImage { get; set; }
        public bool ShowProductsFromTheseCategoryInHomePage { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
