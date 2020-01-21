using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInzynierska.Data.Migrations
{
    public partial class EmialToEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emial",
                table: "Orders",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "Emial");
        }
    }
}
