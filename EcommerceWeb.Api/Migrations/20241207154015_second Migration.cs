using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductCatalog_ProductCatalogProductID",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductCatalogProductID",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductCatalogProductID",
                table: "ProductImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductID",
                table: "ProductImages",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductCatalog_ProductID",
                table: "ProductImages",
                column: "ProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductCatalog_ProductID",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductID",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductCatalogProductID",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductCatalogProductID",
                table: "ProductImages",
                column: "ProductCatalogProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductCatalog_ProductCatalogProductID",
                table: "ProductImages",
                column: "ProductCatalogProductID",
                principalTable: "ProductCatalog",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
