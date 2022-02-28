using Microsoft.EntityFrameworkCore.Migrations;

namespace Eon.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "int",
                table: "Item",
                newName: "ItemPrice");

            migrationBuilder.RenameColumn(
                name: "varchar(20)",
                table: "Item",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "int",
                table: "Customer",
                newName: "CustomerWealth");

            migrationBuilder.RenameColumn(
                name: "varchar(20)",
                table: "Customer",
                newName: "CustomerName");

            migrationBuilder.AddColumn<string>(
                name: "CustomerSurname",
                table: "Customer",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerSurname",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "ItemPrice",
                table: "Item",
                newName: "int");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Item",
                newName: "varchar(20)");

            migrationBuilder.RenameColumn(
                name: "CustomerWealth",
                table: "Customer",
                newName: "int");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Customer",
                newName: "varchar(20)");
        }
    }
}
