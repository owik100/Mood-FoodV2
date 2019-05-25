using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;
using PracaInzynierska.Infrastructure;
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
           

            return View(cart);
        }

        public ActionResult Add(int id)
        {

        }
    }
}