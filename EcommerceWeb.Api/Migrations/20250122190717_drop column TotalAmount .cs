using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class dropcolumnTotalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
