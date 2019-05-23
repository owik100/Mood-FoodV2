using Microsoft.AspNetCore.Mvc;
using PracaInzynierska.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Components
{
    public class CategoryNavigationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CategoryNavigationViewComponent(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _db.Categories.ToList();

            return View(categories);
        }
    }
}
