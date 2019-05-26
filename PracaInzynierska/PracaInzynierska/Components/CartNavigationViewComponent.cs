using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Infrastructure;
using PracaInzynierska.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Components
{
    public class CartNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartProductViewModel> cartProducts= new List<CartProductViewModel>();
            int quantity = 0;

            if (HttpContext.Session.GetString(Constans.SessionCartKey) != null)
            {
                cartProducts = HttpContext.Session.GetObjectFromJson<List<CartProductViewModel>>(Constans.SessionCartKey);

                 quantity = cartProducts.Sum(x => x.Quantity);
            }

            return View(quantity);
        }
    }
}
