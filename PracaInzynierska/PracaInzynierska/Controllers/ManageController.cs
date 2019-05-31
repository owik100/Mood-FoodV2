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
        public ActionResult ProductCreate()
        {
            var categories = _db.Categories.ToList();

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public ActionResult ProductCreate(Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //Wczytano nowy obrazek
                if (file != null)
                {
                    var filename = file.FileName;
                    var imagesPhotoPath = ConfigurationManager.AppSetting["ProductsImagePath"];
                    var rootFolderPath = _environment.WebRootPath;
                    var relativePath = imagesPhotoPath + filename;
                    var path = rootFolderPath + relativePath;

                    product.NameOfImage = file.FileName;
                    using (var photoFile = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(photoFile);
                    }

                    product.NameOfImage = filename;
                }

                //Wybrano produkt dnia - usuwamy poprzedni
                if (product.ProductOfTheDay)
                {
                    var oldProduct = _db.Products.Where(x => x.ProductOfTheDay).FirstOrDefault();
                    oldProduct.ProductOfTheDay = false;
                    _db.Entry(oldProduct).State = EntityState.Modified;
                    _db.SaveChanges();
                }

                _db.Products.Add(product);
                _db.SaveChanges();

                TempData["Message"] = "Sukces! Dodano produkt";
                return RedirectToAction("Index");

            }

            var categories = _db.Categories.ToList();
            ViewBag.Categories = categories;

            return View(product);
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


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ProductEdit(Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //Wczytano nowy obrazek
                if (file != null)
                {
                    var filename = file.FileName;
                    var imagesPhotoPath = ConfigurationManager.AppSetting["ProductsImagePath"];
                    var rootFolderPath = _environment.WebRootPath;
                    var relativePath = imagesPhotoPath + filename;
                    var path = rootFolderPath + relativePath;

                    product.NameOfImage = file.FileName;
                    using (var photoFile = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(photoFile);
                    }

                    product.NameOfImage = filename;
                }

                //Wybrano produkt dnia - usuwamy poprzedni
                if (product.ProductOfTheDay)
                {
                    var oldProduct = _db.Products.Where(x => x.ProductOfTheDay).FirstOrDefault();
                    oldProduct.ProductOfTheDay = false;
                    _db.Entry(oldProduct).State = EntityState.Modified;
                    _db.SaveChanges();
                }

                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();

                TempData["Message"] = "Sukces! zaktualizowano produkt";
                return RedirectToAction("Index");

            }

            var categories = _db.Categories.ToList();
            ViewBag.Categories = categories;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDelete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                TempData["Message"] = "Sukces! Usunięto produkt";
            }
            return RedirectToAction("Index");
        }

    }
}

