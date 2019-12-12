using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PracaInzynierska.Data;
using PracaInzynierska.Infrastructure;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _db;
        private IServiceProvider _serviceProvider;
        private IMyEmailSender _emailSender;

        public CartController(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider, IMyEmailSender emailSender)
        {
            _db = applicationDbContext;
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
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
            if (item != null)
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

            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userId = userManager.GetUserId(HttpContext.User);
            ApplicationUser applicationUser = userManager.FindByIdAsync(userId).Result;

            if (applicationUser != null)
            {
                Order orderUser = new Order
                {
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    City = applicationUser.City,
                    Street = applicationUser.Street,
                    Emial = applicationUser.Email,
                    HouseNumber = applicationUser.HouseNumber,
                    PhoneNumber = applicationUser.PhoneNumber,
                    ZIPCode = applicationUser.ZIPCode,                 

                };

                return View(orderUser);
            }

            Order order = new Order();
            return View(order);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Order(Order order)
        {
            if (ModelState.IsValid)
            {
                List<CartProductViewModel> shoppingCart = GetShoppingCart();

                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var item in shoppingCart)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        PurchasePrice = item.Value,
                        Product = _db.Products.Find(item.Product.ProductId)
                    };

                    orderItems.Add(orderItem);
                }

                bool distanceOk = CheckDistance(order);

                if(distanceOk)
                {
                    order.OrderDate = DateTime.Now;
                    order.OrderItem = orderItems;
                    order.OrderValue = CartTotalValue(shoppingCart);

                    //Dodaj zamówienie do zalogowanego użytkownika, jeżeli jest zalogowany
                    var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var userId = userManager.GetUserId(HttpContext.User);

                    if (userId != null)
                    {
                        order.UserID = userId;
                    }

                    //Zapisz w bazie
                    _db.Orders.Add(order);
                    _db.SaveChanges();

                    //Wyślij potwierdzenie emailem
                    StringBuilder products = new StringBuilder();

                    foreach (var item in order.OrderItem)
                    {
                        products.Append(item.Product.Name + " - " + item.Quantity + "szt.\n");
                    }

                    StringBuilder message = new StringBuilder();
                    message.Append("Dziękujemy za złożene zamówienia!\n\n");
                    message.Append(products);
                    message.Append("Całkowita wartość zamówienia: " + order.OrderValue.ToString("C"));

                    await _emailSender.SendEmailAsync(order.Emial, "Złożono zamówienie", message.ToString());


                    //Usuń koszyk
                    EmptyCart();

                    //Pokaż potwierdzenie
                    TempData["OrderComplete"] = "Zamówienie złożone!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["BigDistance"] = "Odległość od restauracji zbyt duża!";
                    return View(order);
                }

              
            }
            else
                return View(order);
        }

        private bool CheckDistance(Order order)
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            restaurants = _db.Restaurants.ToList();

            foreach (var item in restaurants)
            {

                //Zamówienie z tego samego miasta co restauracja
                if (item.City == order.City)
                    return true;

                var json = new WebClient().DownloadString($"https://www.dystans.org/route.json?stops={item.City}|{order.City}");
                dynamic city = JsonConvert.DeserializeObject(json);

                string distance = city.distance;

                

                if ((Convert.ToInt32(distance) <= Convert.ToInt32(item.MaxDistance) && (Convert.ToInt32(distance) !=0)))
                    return true;
            }

            return false;
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