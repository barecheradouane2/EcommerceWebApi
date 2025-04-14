using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderModal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commune",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Wilaya",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CommuneID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WilayaID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CommuneID",
                table: "Orders",
                column: "CommuneID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WilayaID",
                table: "Orders",
                column: "WilayaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Commune_CommuneID",
                table: "Orders",
                column: "CommuneID",
                principalTable: "Commune",
                principalColumn: "CommuneID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Wilaya_WilayaID",
                table: "Orders",
                column: "WilayaID",
                principalTable: "Wilaya",
                principalColumn: "WilayaID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Commune_CommuneID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Wilaya_WilayaID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CommuneID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WilayaID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CommuneID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WilayaID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Commune",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Wilaya",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
