using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class addingwilayaandshipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingID",
                table: "Wilaya",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WilayaID",
                table: "ShippingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingInfo_WilayaID",
                table: "ShippingInfo",
                column: "WilayaID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfo_Wilaya_WilayaID",
                table: "ShippingInfo",
                column: "WilayaID",
                principalTable: "Wilaya",
                principalColumn: "WilayaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfo_Wilaya_WilayaID",
                table: "ShippingInfo");

            migrationBuilder.DropIndex(
                name: "IX_ShippingInfo_WilayaID",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "ShippingID",
                table: "Wilaya");

            migrationBuilder.DropColumn(
                name: "WilayaID",
                table: "ShippingInfo");
        }
    }
}
