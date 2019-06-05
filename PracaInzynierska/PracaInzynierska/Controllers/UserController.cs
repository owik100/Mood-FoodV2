using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracaInzynierska.Data;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.ViewModels;

namespace PracaInzynierska.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _db = applicationDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var userOrders = _db.Orders.
               Where(x => x.UserID == userId)
               .Include(y => y.OrderItem)
               .ToList();

            return View(userOrders);
        }

        public IActionResult OrderDetails(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var orderDetails = _db.OrderItems
                .Where(x => x.OrderId == id)
                .Include(y => y.Product)
                .ToList();

            var order = _db.Orders.
                Where(x => x.OrderId == id)
                .FirstOrDefault();

            //Czy to na pewno zmaówienie zalogowanego usera?
            if (order.UserID == userId)
            {
                OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel
                {
                    Order = order,
                    OrderItems = orderDetails,
                };

                return View(orderDetailsViewModel);
            }
            else
            {
                return Forbid();
            }

           
        }
    }
}