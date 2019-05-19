using PracaInzynierska.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.ViewModels
{
    public class IndexViewModel
    {
        public List<Product> RandomProducts { get; set; }
        public Product ProductOfTheDay { get; set; }

    }
}
