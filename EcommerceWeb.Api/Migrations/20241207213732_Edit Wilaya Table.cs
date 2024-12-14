using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class EditWilayaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commune_Wilaya_WilayaID1",
                table: "Commune");

            migrationBuilder.DropIndex(
                name: "IX_Commune_WilayaID1",
                table: "Commune");

            migrationBuilder.DropColumn(
                name: "WilayaID1",
                table: "Commune");

            migrationBuilder.AlterColumn<int>(
                name: "WilayaID",
                table: "Commune",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Commune_WilayaID",
                table: "Commune",
                column: "WilayaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Commune_Wilaya_WilayaID",
                table: "Commune",
                column: "WilayaID",
                principalTable: "Wilaya",
                principalColumn: "WilayaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commune_Wilaya_WilayaID",
                table: "Commune");

            migrationBuilder.DropIndex(
                name: "IX_Commune_WilayaID",
                table: "Commune");

            migrationBuilder.AlterColumn<string>(
                name: "WilayaID",
                table: "Commune",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WilayaID1",
                table: "Commune",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commune_WilayaID1",
                table: "Commune",
                column: "WilayaID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Commune_Wilaya_WilayaID1",
                table: "Commune",
                column: "WilayaID1",
                principalTable: "Wilaya",
                principalColumn: "WilayaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
