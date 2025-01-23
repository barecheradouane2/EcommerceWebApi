using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class renametheTotalAmountcolumnversion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
          name: "TotalAmount",
          table: "Orders",
          newName: "TotalPrice");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
           name: "TotalPrice",
           table: "Orders",
           newName: "TotalAmount");

        }
    }
}
