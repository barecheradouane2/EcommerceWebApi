﻿// <auto-generated />
using System;
using EcommerceWeb.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceWeb.Api.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20250407211316_kksif")]
    partial class kksif
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryID");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Commune", b =>
                {
                    b.Property<int>("CommuneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommuneID"));

                    b.Property<string>("CommuneName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WilayaID")
                        .HasColumnType("int");

                    b.HasKey("CommuneID");

                    b.HasIndex("WilayaID");

                    b.ToTable("Commune");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.DiscountCodes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MinPurchaseAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("DiscountCodes");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.OrderItems", b =>
                {
                    b.Property<int>("OrderItemsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemsID"));

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemsID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Orders", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<string>("Commune")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountCodeID")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<int>("ShippingID")
                        .HasColumnType("int");

                    b.Property<int>("ShippingStatus")
                        .HasColumnType("int");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Wilaya")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.HasIndex("DiscountCodeID");

                    b.HasIndex("ShippingID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductCatalog", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("ProductCatalog");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductColorVariant", b =>
                {
                    b.Property<int>("ProductColorVariantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductColorVariantID"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductSizeID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductColorVariantID");

                    b.HasIndex("ProductSizeID");

                    b.ToTable("ProductColorVariant");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductImages", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"));

                    b.Property<int>("ImageOrder")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductSize", b =>
                {
                    b.Property<int>("ProductSizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductSizeID"));

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductSizeID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductSize");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ShippingInfo", b =>
                {
                    b.Property<int>("ShippingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingID"));

                    b.Property<decimal>("HomeDeliveryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OfficeDeliveryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShipingStatus")
                        .HasColumnType("int");

                    b.Property<string>("WilayaFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WilayaTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShippingID");

                    b.ToTable("ShippingInfo");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Wilaya", b =>
                {
                    b.Property<int>("WilayaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WilayaID"));

                    b.Property<string>("WilayaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WilayaID");

                    b.ToTable("Wilaya");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Commune", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.Wilaya", "Wilaya")
                        .WithMany()
                        .HasForeignKey("WilayaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wilaya");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.OrderItems", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.Orders", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceWeb.Api.Models.Domain.ProductCatalog", "ProductCatalog")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductCatalog");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Orders", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.DiscountCodes", "DiscountCodes")
                        .WithMany()
                        .HasForeignKey("DiscountCodeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceWeb.Api.Models.Domain.ShippingInfo", "ShippingInfo")
                        .WithMany()
                        .HasForeignKey("ShippingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DiscountCodes");

                    b.Navigation("ShippingInfo");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductCatalog", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.Category", "Category")
                        .WithMany("ProductCatalog")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductColorVariant", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.ProductSize", "ProductSize")
                        .WithMany("ProductColorVariant")
                        .HasForeignKey("ProductSizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductSize");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductImages", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.ProductCatalog", "ProductCatalog")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCatalog");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductSize", b =>
                {
                    b.HasOne("EcommerceWeb.Api.Models.Domain.ProductCatalog", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Category", b =>
                {
                    b.Navigation("ProductCatalog");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.Orders", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductCatalog", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("EcommerceWeb.Api.Models.Domain.ProductSize", b =>
                {
                    b.Navigation("ProductColorVariant");
                });
#pragma warning restore 612, 618
        }
    }
}
