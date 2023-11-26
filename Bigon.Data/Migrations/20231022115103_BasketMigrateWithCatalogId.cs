using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigon.WebUI.Migrations
{
    public partial class BasketMigrateWithCatalogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Basket",
                newName: "CatalogId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_ProductId",
                table: "Basket",
                newName: "IX_Basket_CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ProductCatalog_CatalogId",
                table: "Basket",
                column: "CatalogId",
                principalTable: "ProductCatalog",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ProductCatalog_CatalogId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "CatalogId",
                table: "Basket",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_CatalogId",
                table: "Basket",
                newName: "IX_Basket_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
