using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class handleshippingwithorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders",
                column: "ShippingID",
                principalTable: "ShippingInfo",
                principalColumn: "ShippingID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingID",
                table: "Orders");

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
