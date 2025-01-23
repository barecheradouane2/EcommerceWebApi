using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class totalAmounttototalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<int>(
                name: "OrderID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_OrderID",
                table: "Orders",
                column: "OrderID",
                principalTable: "ShippingInfo",
                principalColumn: "ShippingID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_OrderID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.AlterColumn<int>(
                name: "OrderID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingID",
                table: "Orders",
                column: "ShippingID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductCatalog_ProductID",
                table: "OrderItems",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders",
                column: "ShippingID",
                principalTable: "ShippingInfo",
                principalColumn: "ShippingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
