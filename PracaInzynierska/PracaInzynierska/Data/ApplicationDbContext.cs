using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracaInzynierska.Models.Entities;

namespace PracaInzynierska.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<Category> categories = new List<Category>
            {
                new Category(){CategoryId=1, Name="Burgery", Description="Tylko z najwyższej jakości wołowiny!", NameOfImage="Burgery.jpeg", ShowProductsFromTheseCategoryInHomePage = true},
                new Category(){CategoryId=2, Name="Sałatki", Description="Najświeższe składniki to nasza tajemnica!", NameOfImage="Sałatki.jpeg", ShowProductsFromTheseCategoryInHomePage = true},
                new Category(){CategoryId=3, Name="Pizze", Description="Na najgrubszym cieście w mieście!", NameOfImage="Pizze.jpeg", ShowProductsFromTheseCategoryInHomePage = true},
                new Category(){CategoryId=4, Name="Dodatki", Description="Super dodatki za super cenę!", NameOfImage="Dodatki.jpeg", ShowProductsFromTheseCategoryInHomePage = false},
            };

            List<Product> products = new List<Product>
            {
                new Product(){ProductId=1, Name="Hamburger", Description="Klasyczny hamburger, plaster wołowiny, cebula i ogórek", CategoryId=1, NameOfImage="Hamburger.jpeg", Price=2.99m, Hidden=false, ProductOfTheDay=true},
                new Product(){ProductId=2, Name="Cheeseburger", Description="Pyszny hamburger z dodatkiem plasterka sera", CategoryId=1, NameOfImage="Cheeseburger.jpeg", Price=3.99m, Hidden=false},
                new Product(){ProductId=3, Name="Sałatka meksykańska", Description="Z ananasem, porem, kukurydzą, papryką, serem żółtym i czerwoną fasolką", CategoryId=2, NameOfImage="SalatkaMeksykanska.jpeg", Price=5.49m, Hidden=false},
                new Product(){ProductId=4, Name="Sałatka z kurczakiem", Description="Mieszanka sałat z burakiem i kawałkami kurczaka w złocistej panierce", CategoryId=2, NameOfImage="SalatkaZKurczakiem.jpeg", Price=4.99m, Hidden=false},
                new Product(){ProductId=5, Name="Pizza Margherita", Description="Pizza z sosem pomidorowym i tartym serem mozzarella.", CategoryId=3, NameOfImage="PizzaMargherita.jpeg", Price=15.99m, Hidden=false},
                new Product(){ProductId=6, Name="Pizza Wiejska", Description="Pizza z sosem pomidorowym, szynką, boczkiem, kiełbasą i cebulą.", CategoryId=3, NameOfImage="PizzaWiejska.jpeg", Price=21.99m, Hidden=false},
                new Product(){ProductId=7, Name="Keczup", Description="Z dojrzewających w słońcu pomidorów.", CategoryId=4, NameOfImage="Keczup.jpeg", Price=0.99m, Hidden=false},
                new Product(){ProductId=8, Name="Musztarda", Description="Bardzo ostra!", CategoryId=4, NameOfImage="Musztarda.jpeg", Price=0.99m, Hidden=false},
            };

       

            builder.Entity<Category>().HasData(categories);
            builder.Entity<Product>().HasData(products);

            //builder.Entity<Order>()
            //.HasForeignKey(p => p.UserID);
        }
    }
}
