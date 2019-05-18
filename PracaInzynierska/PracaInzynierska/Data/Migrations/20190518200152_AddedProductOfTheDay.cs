using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInzynierska.Data.Migrations
{
    public partial class AddedProductOfTheDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProductOfTheDay",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOfTheDay",
                table: "Products");
        }
    }
}
