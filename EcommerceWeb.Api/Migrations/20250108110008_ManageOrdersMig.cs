using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class ManageOrdersMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdersOrderID",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrdersOrderID",
                table: "OrderItems",
                column: "OrdersOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrdersOrderID",
                table: "OrderItems",
                column: "OrdersOrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrdersOrderID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrdersOrderID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrdersOrderID",
                table: "OrderItems");
        }
    }
}
