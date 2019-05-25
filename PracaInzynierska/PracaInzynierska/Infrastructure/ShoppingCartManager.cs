using PracaInzynierska.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Infrastructure
{
    public class ShoppingCartManager
    {
        public List<CartProductViewModel> GetShoppingCart()
        {
            List<CartProductViewModel> cartProducts = new List<CartProductViewModel>();

            //Pobierz koszyk z sesji, jak nie ma to stwórz
            if (HttpContext.Session.GetString(Constans.SessionCartKey) != null)
            {
                cartProducts = HttpContext.Session.GetObjectFromJson<List<CartProductViewModel>>(Constans.SessionCartKey);
            }
            else
            {
                HttpContext.Session.SetObjectAsJson(Constans.SessionCartKey, cartProducts);
            }

            CartViewModel cart = new CartViewModel { CartProductViewModels = cartProducts };

            return cart;
        }

        //public void AddToShoppingCart(int id)
        //{

        //}

        //public void DeleteFromCart(int id)
        //{

        //}
    }
}
