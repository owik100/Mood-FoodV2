using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;
using PracaInzynierska.Infrastructure;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _db;

        public CartController(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public IActionResult Index()
        {

            List<CartProductViewModel> cartProducts = GetShoppingCart();
            decimal totalValue = CartTotalValue(cartProducts);
            CartViewModel cart = new CartViewModel { CartProductViewModels = cartProducts, TotalValue = totalValue };

            return View(cart);
        }

        public IActionResult Add(int id)
        {
            List<CartProductViewModel> cartProducts = GetShoppingCart();

            CartProductViewModel item = cartProducts.Find(x => x.Product.ProductId == id);

            //Jeżeli w koszyku nie ma dodawanego produktu to go dodaj, inaczej zwiększ jego ilość
            if(item != null)
            {
                item.Quantity++;
                item.Value = item.Quantity * item.Product.Price;
            }
            else
            {
                Product product = _db.Products.Find(id);
                cartProducts.Add(new CartProductViewModel { Product = product, Quantity = 1, Value = product.Price });
            }

            HttpContext.Session.SetObjectAsJson(Constans.SessionCartKey, cartProducts);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            List<CartProductViewModel> cartProducts = GetShoppingCart();

            CartProductViewModel item = cartProducts.Find(x => x.Product.ProductId == id);

            //Usuń produkt z koszyka lub zmniejsz jego ilość
            if (item != null)
            {
                item.Quantity--;
                item.Value = item.Product.Price * item.Quantity;

                if (item.Quantity <= 0)
                {
                    cartProducts.Remove(item);
                }
            }

            HttpContext.Session.SetObjectAsJson(Constans.SessionCartKey, cartProducts);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Order()
        {
            Order order = new Order();

            return View(order);
        }

        [HttpPost]
        public ActionResult Order(Order order)
        {
            if (ModelState.IsValid)
            {
                List<CartProductViewModel> shoppingCart = GetShoppingCart();
             
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var item in shoppingCart)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        Product = item.Product,
                        Quantity = item.Quantity,
                        PurchasePrice = item.Value
                    };

                    orderItems.Add(orderItem);
                }

                order.OrderDate = DateTime.Now;
                order.OrderItem = orderItems;
                order.OrderValue = CartTotalValue(shoppingCart);

                _db.Orders.Add(order);
                _db.SaveChanges();

                //Usuń koszyk
                EmptyCart();

                //Pokaż potwierdzenie
                ViewData["OrderComplete"] = "Zamówienie złożone!";
                return RedirectToAction("Index");
            }
            else
                return View(order);
        }

        private void EmptyCart()
        {
            // HttpContext.Session.Clear();
            HttpContext.Session.Remove(Constans.SessionCartKey);
        }

        private List<CartProductViewModel> GetShoppingCart()
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

            return cartProducts;
        }

        private decimal CartTotalValue(List<CartProductViewModel> cart)
        {
            decimal totalValue = 0;

            foreach (var item in cart)
            {
                totalValue += item.Value;
            }

            return totalValue;
        }

    }
}