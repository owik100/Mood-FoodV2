using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Infrastructure
{
    public static class UrlExtensions
    {

        public static string ImagePath(this IUrlHelper helper, string imageName)
        {
            string path = ConfigurationManager.AppSetting["ImagePath"];
            return Path.Combine(path + imageName);
        }

        public static string ProductImagePath(this IUrlHelper helper, string imageName)
        {
            string path = ConfigurationManager.AppSetting["ProductsImagePath"];
            return Path.Combine(path + imageName);
        }

        public static string CategoriesImagePath(this IUrlHelper helper, string imageName)
        {
            string path = ConfigurationManager.AppSetting["CategoriesImagePath"];
            return Path.Combine(path + imageName);
        }
    }
}
