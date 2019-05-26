using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;
using PracaInzynierska.Models;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }
        public IActionResult Index()
        {

            Product productOfTheDay = _db.Products.Where(x => x.ProductOfTheDay == true).FirstOrDefault();

            if(productOfTheDay == null)
            {
                _db.Products.First().ProductOfTheDay = true;
                _db.SaveChanges();

                productOfTheDay = _db.Products.First();
            }

            List<Product> randomProducts;
            randomProducts = _db.Products
                .Where(x => x.Category.ShowProductsFromTheseCategoryInHomePage == true && x.ProductOfTheDay == false)
                .OrderBy(x => Guid.NewGuid())
                .Take(4).ToList();

            

            IndexViewModel indexViewModel = new IndexViewModel { RandomProducts = randomProducts, ProductOfTheDay = productOfTheDay };

            return View(indexViewModel);
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
