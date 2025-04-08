using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSizesAndColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSize",
                columns: table => new
                {
                    ProductSizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSize", x => x.ProductSizeID);
                    table.ForeignKey(
                        name: "FK_ProductSize_ProductCatalog_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductCatalog",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColorVariant",
                columns: table => new
                {
                    ProductColorVariantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductSizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorVariant", x => x.ProductColorVariantID);
                    table.ForeignKey(
                        name: "FK_ProductColorVariant_ProductSize_ProductSizeID",
                        column: x => x.ProductSizeID,
                        principalTable: "ProductSize",
                        principalColumn: "ProductSizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorVariant_ProductSizeID",
                table: "ProductColorVariant",
                column: "ProductSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_ProductID",
                table: "ProductSize",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColorVariant");

            migrationBuilder.DropTable(
                name: "ProductSize");
        }
    }
}
