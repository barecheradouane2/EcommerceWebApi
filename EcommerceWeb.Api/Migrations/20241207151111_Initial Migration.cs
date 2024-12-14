﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinPurchaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingInfo",
                columns: table => new
                {
                    ShippingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WilayaFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WilayaTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingStatus = table.Column<int>(type: "int", nullable: false),
                    HomeDeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfficeDeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInfo", x => x.ShippingID);
                });

            migrationBuilder.CreateTable(
                name: "Wilaya",
                columns: table => new
                {
                    WilayaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WilayaName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wilaya", x => x.WilayaID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCatalog",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalog", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_ProductCatalog_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wilaya = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commune = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountCodeID = table.Column<int>(type: "int", nullable: false),
                    ShippingID = table.Column<int>(type: "int", nullable: false),
                    ShippingStatus = table.Column<int>(type: "int", nullable: false),
                    ShippingInfoShippingID = table.Column<int>(type: "int", nullable: false),
                    DiscountCodesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountCodes_DiscountCodesID",
                        column: x => x.DiscountCodesID,
                        principalTable: "DiscountCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingInfo_ShippingInfoShippingID",
                        column: x => x.ShippingInfoShippingID,
                        principalTable: "ShippingInfo",
                        principalColumn: "ShippingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commune",
                columns: table => new
                {
                    CommuneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommuneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WilayaID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WilayaID1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commune", x => x.CommuneID);
                    table.ForeignKey(
                        name: "FK_Commune_Wilaya_WilayaID1",
                        column: x => x.WilayaID1,
                        principalTable: "Wilaya",
                        principalColumn: "WilayaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItemsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductCatalogProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemsID);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductCatalog_ProductCatalogProductID",
                        column: x => x.ProductCatalogProductID,
                        principalTable: "ProductCatalog",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageOrder = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductCatalogProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProductCatalog_ProductCatalogProductID",
                        column: x => x.ProductCatalogProductID,
                        principalTable: "ProductCatalog",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commune_WilayaID1",
                table: "Commune",
                column: "WilayaID1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductCatalogProductID",
                table: "OrderItems",
                column: "ProductCatalogProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountCodesID",
                table: "Orders",
                column: "DiscountCodesID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingInfoShippingID",
                table: "Orders",
                column: "ShippingInfoShippingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_CategoryID",
                table: "ProductCatalog",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductCatalogProductID",
                table: "ProductImages",
                column: "ProductCatalogProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commune");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Wilaya");

            migrationBuilder.DropTable(
                name: "DiscountCodes");

            migrationBuilder.DropTable(
                name: "ShippingInfo");

            migrationBuilder.DropTable(
                name: "ProductCatalog");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}