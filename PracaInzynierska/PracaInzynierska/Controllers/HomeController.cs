using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;
using PracaInzynierska.Models;
using PracaInzynierska.Models.Entities;

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

            List<Product> randomProducts;
            randomProducts = _db.Products
                .Where(x => x.Category.ShowProductsFromTheseCategoryInHomePage == true)
                .OrderBy(x => Guid.NewGuid())
                .Take(4).ToList();

            return View(randomProducts);
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
