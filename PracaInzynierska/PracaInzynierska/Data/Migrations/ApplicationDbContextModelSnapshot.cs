﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracaInzynierska.Data;

namespace PracaInzynierska.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Street")
                        .HasMaxLength(50);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("ZIPCode")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NameOfImage");

                    b.Property<bool>("ShowProductsFromTheseCategoryInHomePage");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Tylko z najwyższej jakości wołowiny!",
                            Name = "Burgery",
                            NameOfImage = "Burgery.jpeg",
                            ShowProductsFromTheseCategoryInHomePage = true
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Najświeższe składniki to nasza tajemnica!",
                            Name = "Sałatki",
                            NameOfImage = "Sałatki.jpeg",
                            ShowProductsFromTheseCategoryInHomePage = true
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Na najgrubszym cieście w mieście!",
                            Name = "Pizze",
                            NameOfImage = "Pizze.jpeg",
                            ShowProductsFromTheseCategoryInHomePage = true
                        },
                        new
                        {
                            CategoryId = 4,
                            Description = "Super dodatki za super cenę!",
                            Name = "Dodatki",
                            NameOfImage = "Dodatki.jpeg",
                            ShowProductsFromTheseCategoryInHomePage = false
                        });
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OptionalDescription");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("OrderValue");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserID");

                    b.Property<string>("ZIPCode")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("OrderId");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Hidden");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameOfImage");

                    b.Property<decimal>("Price");

                    b.Property<bool>("ProductOfTheDay");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Description = "Klasyczny hamburger, plaster wołowiny, cebula i ogórek",
                            Hidden = false,
                            Name = "Hamburger",
                            NameOfImage = "Hamburger.jpeg",
                            Price = 2.99m,
                            ProductOfTheDay = true
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Description = "Pyszny hamburger z dodatkiem plasterka sera",
                            Hidden = false,
                            Name = "Cheeseburger",
                            NameOfImage = "Cheeseburger.jpeg",
                            Price = 3.99m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            Description = "Z ananasem, porem, kukurydzą, papryką, serem żółtym i czerwoną fasolką",
                            Hidden = false,
                            Name = "Sałatka meksykańska",
                            NameOfImage = "SalatkaMeksykanska.jpeg",
                            Price = 5.49m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            Description = "Mieszanka sałat z burakiem i kawałkami kurczaka w złocistej panierce",
                            Hidden = false,
                            Name = "Sałatka z kurczakiem",
                            NameOfImage = "SalatkaZKurczakiem.jpeg",
                            Price = 4.99m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            Description = "Pizza z sosem pomidorowym i tartym serem mozzarella.",
                            Hidden = false,
                            Name = "Pizza Margherita",
                            NameOfImage = "PizzaMargherita.jpeg",
                            Price = 15.99m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            Description = "Pizza z sosem pomidorowym, szynką, boczkiem, kiełbasą i cebulą.",
                            Hidden = false,
                            Name = "Pizza Wiejska",
                            NameOfImage = "PizzaWiejska.jpeg",
                            Price = 21.99m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 4,
                            Description = "Z dojrzewających w słońcu pomidorów.",
                            Hidden = false,
                            Name = "Keczup",
                            NameOfImage = "Keczup.jpeg",
                            Price = 0.99m,
                            ProductOfTheDay = false
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            Description = "Bardzo ostra!",
                            Hidden = false,
                            Name = "Musztarda",
                            NameOfImage = "Musztarda.jpeg",
                            Price = 0.99m,
                            ProductOfTheDay = false
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PracaInzynierska.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.Order", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.OrderItem", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PracaInzynierska.Models.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PracaInzynierska.Models.Entities.Product", b =>
                {
                    b.HasOne("PracaInzynierska.Models.Entities.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
