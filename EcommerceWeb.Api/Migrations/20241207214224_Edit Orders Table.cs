using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class EditOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodesID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingInfoShippingID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DiscountCodesID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingInfoShippingID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountCodesID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingInfoShippingID",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountCodeID",
                table: "Orders",
                column: "DiscountCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingID",
                table: "Orders",
                column: "ShippingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeID",
                table: "Orders",
                column: "DiscountCodeID",
                principalTable: "DiscountCodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders",
                column: "ShippingID",
                principalTable: "ShippingInfo",
                principalColumn: "ShippingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DiscountCodeID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingID",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodesID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingInfoShippingID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountCodesID",
                table: "Orders",
                column: "DiscountCodesID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingInfoShippingID",
                table: "Orders",
                column: "ShippingInfoShippingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodesID",
                table: "Orders",
                column: "DiscountCodesID",
                principalTable: "DiscountCodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingInfoShippingID",
                table: "Orders",
                column: "ShippingInfoShippingID",
                principalTable: "ShippingInfo",
                principalColumn: "ShippingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
