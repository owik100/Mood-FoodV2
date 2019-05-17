using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInzynierska.Data.Migrations
{
    public partial class AddedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NameOfImage = table.Column<string>(nullable: true),
                    ShowProductsFromTheseCategoryInHomePage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    ZIPCode = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Emial = table.Column<string>(nullable: true),
                    OptionalDescription = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrderValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NameOfImage = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "NameOfImage", "ShowProductsFromTheseCategoryInHomePage" },
                values: new object[,]
                {
                    { 1, "Tylko z najwyższej jakości wołowiny!", "Burgery", "Burgery.jpeg", true },
                    { 2, "Najświeższe składniki to nasza tajemnica!", "Sałatki", "Sałatki.jpeg", true },
                    { 3, "Na najgrubszym cieście w mieście!", "Pizze", "Pizze.jpeg", true },
                    { 4, "Super dodatki za super cenę!", "Dodatki", "Dodatki.jpeg", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Hidden", "Name", "NameOfImage", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Klasyczny hamburger, plaster wołowiny, cebula i ogórek", false, "Hamburger", "Hamburger.jpeg", 2.99m },
                    { 2, 1, "Pyszny hamburger z dodatkiem plasterka sera", false, "Cheeseburger", "Cheeseburger.jpeg", 3.99m },
                    { 3, 2, "Z ananasem, porem, kukurydzą, papryką, serem żółtym i czerwoną fasolką", false, "Sałatka meksykańska", "SalatkaMeksykanska.jpeg", 5.49m },
                    { 4, 2, "Mieszanka sałat z burakiem i kawałkami kurczaka w złocistej panierce", false, "Sałatka z kurczakiem", "SalatkaZKurczakiem.jpeg", 4.99m },
                    { 5, 3, "Pizza z sosem pomidorowym i tartym serem mozzarella.", false, "Pizza Margherita", "PizzaMargherita.jpeg", 15.99m },
                    { 6, 3, "Pizza z sosem pomidorowym, szynką, boczkiem, kiełbasą i cebulą.", false, "Pizza Wiejska", "PizzaWiejska.jpeg", 21.99m },
                    { 7, 4, "Z dojrzewających w słońcu pomidorów.", false, "Keczup", "Keczup.jpeg", 0.99m },
                    { 8, 4, "Bardzo ostra!", false, "Musztarda", "Musztarda.jpeg", 0.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
