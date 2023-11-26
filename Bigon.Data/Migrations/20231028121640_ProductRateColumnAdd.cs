using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigon.WebUI.Migrations
{
    public partial class ProductRateColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductRates",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRates", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductRates_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRates_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_ProductId",
                table: "ProductRates",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRates");
        }
    }
}
