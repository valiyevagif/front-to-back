using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigon.WebUI.Migrations
{
    public partial class ProductCatalogMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCatalog_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCatalog_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCatalog_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCatalog_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(type: "int", nullable: false),
                    DocumentNo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStock_ProductCatalog_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "ProductCatalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_ColorId",
                table: "ProductCatalog",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_MaterialId",
                table: "ProductCatalog",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_ProductId_SizeId_ColorId_MaterialId",
                table: "ProductCatalog",
                columns: new[] { "ProductId", "SizeId", "ColorId", "MaterialId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_SizeId",
                table: "ProductCatalog",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_CatalogId",
                table: "ProductStock",
                column: "CatalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "ProductCatalog");
        }
    }
}
