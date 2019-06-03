﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using PracaInzynierska.Data;
using PracaInzynierska.Infrastructure;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _environment;
        private IServiceProvider _serviceProvider;

        public ManageController(ApplicationDbContext applicationDbContext, IHostingEnvironment environment, IServiceProvider serviceProvider)
        {
            _db = applicationDbContext;
            _environment = environment;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllUsers()
        {
            var users = _db.Users
                 .Include(x => x.Orders)
                 .ToList();
            return View(users);
        }

        public IActionResult UserOrders(string Id)
        {
            var user = _db.Users.Find(Id);

            var orders = _db.Orders.
                Where(x => x.UserID == Id)
                .Include(y => y.OrderItem)
                .ToList();

            UserOrdersViewModel userOrdersViewModel = new UserOrdersViewModel
            {
                ApplicationUser = user,
                Orders = orders
            };

            return View(userOrdersViewModel);
        }


        public IActionResult OrderDetails(int id)
        {

            //TODO Zrobic viewModel i wyswietlic
            var orderDetails = _db.OrderItems
                .Where(x => x.OrderId == id)
                .Include(y=>y.Product)
                .ToList();

            var order = _db.Orders.
                Where(x => x.OrderId == id)
                .FirstOrDefault();

            return View(orderDetails);
        }

        public async Task<IActionResult> UserAdmin(string Id)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(Id);
            var IsAdmin = await userManager.IsInRoleAsync(user, "Admin");

            ViewBag.IsAdmin = IsAdmin;
            return View(user);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UserAdmin(ApplicationUser user)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var IsAdmin = await userManager.IsInRoleAsync(user, "Admin");

            //Czy zalogowany admin nie próbuje odebrać sobie praw?
            var currentUserId = userManager.GetUserId(HttpContext.User);
            if (currentUserId == user.Id)
            {
                TempData["MessageUser"] = "Błąd! Nie można zmienić uprawnień samemu sobie!";
                return RedirectToAction("AllUsers");
            }

            var userEdit = await userManager.FindByIdAsync(user.Id);
            //Jeżeli user był adminem, usuń, inaczej ustaw rolę
            if (IsAdmin)
            {

                await userManager.RemoveFromRoleAsync(userEdit, "Admin");
            }
            else
            {
                await userManager.AddToRoleAsync(userEdit, "Admin");
            }
            //await userManager.UpdateAsync(user);
            TempData["MessageUser"] = "Sukces! Zmieniono uprawnienia";
            return RedirectToAction("AllUsers");
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
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("AllProducts");

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
                return RedirectToAction("AllProducts");

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

                //Usuń obrazek
                if (product.NameOfImage != null)
                {
                    try
                    {
                        var imagesPhotoPath = ConfigurationManager.AppSetting["ProductsImagePath"];
                        var rootFolderPath = _environment.WebRootPath;
                        var relativePath = imagesPhotoPath + product.NameOfImage;
                        var path = rootFolderPath + relativePath;

                        System.IO.File.Delete(path);
                    }
                    catch (Exception)
                    {

                    }

                }
                TempData["Message"] = "Sukces! Usunięto produkt";
            }
            return RedirectToAction("AllProducts");
        }

    }
}

