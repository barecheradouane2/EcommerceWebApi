using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiscountIDAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DiscountCodeID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountCodeID",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodeID",
                table: "OrderItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountCodeID",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodeID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountCodeID",
                table: "Orders",
                column: "DiscountCodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeID",
                table: "Orders",
                column: "DiscountCodeID",
                principalTable: "DiscountCodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
