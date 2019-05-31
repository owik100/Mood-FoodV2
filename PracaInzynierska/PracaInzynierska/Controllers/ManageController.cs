using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using PracaInzynierska.Data;
using PracaInzynierska.Infrastructure;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    public class ManageController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _environment;

        public ManageController(ApplicationDbContext applicationDbContext, IHostingEnvironment environment)
        {
            _db = applicationDbContext;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return RedirectToAction("AllProducts");

        }

        public ActionResult AllProducts()
        {
            var products = _db.Products
                .Include("Category")
                .ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult ProductEdit(int id)
        {

            var product = _db.Products
           .Include("Category")
           .Where(x => x.ProductId == id)
           .FirstOrDefault();

            var categories = _db.Categories.ToList();

            ViewBag.Categories = categories;

            return View(product);
        }

        [HttpPost]
        public ActionResult ProductEdit(Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var filename = file.FileName;
                    var rootFolderPath = _environment.WebRootPath;
                    var ImagefolderPath = ConfigurationManager.AppSetting["ProductsImagePath"];
                    var path = Path.Combine(rootFolderPath, ImagefolderPath + filename);

                    product.NameOfImage = file.FileName;
                    using (var photoFile = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(photoFile);
                    }
                       
                    product.NameOfImage = filename;
                }
            }
                var categories = _db.Categories.ToList();

            ViewBag.Categories = categories;

            return View(product);
        }

    }
}

