using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class EditOrdersitemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductCatalogProductID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductCatalogProductID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductCatalogProductID",
                table: "OrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                table: "OrderItems",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductID",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductCatalogProductID",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductCatalogProductID",
                table: "OrderItems",
                column: "ProductCatalogProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductCatalogProductID",
                table: "OrderItems",
                column: "ProductCatalogProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
