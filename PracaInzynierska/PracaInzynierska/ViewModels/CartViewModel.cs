using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.ViewModels
{
    public class CartViewModel
    {
        public List<CartProductViewModel> CartProductViewModels { get; set; }
        public decimal TotalValue { get; set; }
    }
}
