using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;

namespace PracaInzynierska.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public ActionResult Categories()
        {
            var categories = _db.Categories.ToList();

            return View(categories);
        }

        public ActionResult Products(string category = "")
        {

            if (category == "")
            {
                var allProducts = _db.Products
                    .Where(x => !x.Hidden)
                    .ToList();

                return View(allProducts);
            }

            var products = _db.Products
                .Where(x => x.Category.Name == category && !x.Hidden)
                .ToList();

            return View(products);
        }
    }
}