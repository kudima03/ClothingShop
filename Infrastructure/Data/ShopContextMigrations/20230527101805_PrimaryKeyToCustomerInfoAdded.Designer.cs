﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.ShopContextMigrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20230527101805_PrimaryKeyToCustomerInfoAdded")]
    partial class PrimaryKeyToCustomerInfoAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Entities.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("brands", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Color", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Hex")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hex");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("colors", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.CustomerInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Surname")
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("customers_info", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.ImageInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ProductColorId")
                        .HasColumnType("bigint")
                        .HasColumnName("product_color_id");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("ProductColorId");

                    b.ToTable("images_urls", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time");

                    b.Property<long>("OrderStatusId")
                        .HasColumnType("bigint")
                        .HasColumnName("order_status_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("order_statuses", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint")
                        .HasColumnName("brand_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("SubcategoryId")
                        .HasColumnType("bigint")
                        .HasColumnName("subcategory_id");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.ProductColor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("ColorId")
                        .HasColumnType("bigint")
                        .HasColumnName("color_id");

                    b.HasKey("Id");

                    b.ToTable("product_colors", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.ProductOption", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<long>("ProductColorId")
                        .HasColumnType("bigint")
                        .HasColumnName("product_color_id");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("size");

                    b.HasKey("Id");

                    b.HasIndex("ProductColorId");

                    b.HasIndex("ProductId");

                    b.ToTable("product_option", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("product_id");

                    b.Property<int>("Rate")
                        .HasColumnType("integer")
                        .HasColumnName("rate");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Section", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("sections", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.Subcategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("subcategories", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<long>("UserTypeId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_type_id");

                    b.HasKey("Id");

                    b.HasIndex("UserTypeId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ApplicationCore.Entities.UserType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("user_types", (string)null);
                });

            modelBuilder.Entity("category_subcategories", b =>
                {
                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.Property<long>("subcategory_id")
                        .HasColumnType("bigint");

                    b.HasKey("category_id", "subcategory_id");

                    b.HasIndex("subcategory_id");

                    b.ToTable("category_subcategories");
                });

            modelBuilder.Entity("orders_product_options", b =>
                {
                    b.Property<long>("order_id")
                        .HasColumnType("bigint");

                    b.Property<long>("product_option_id")
                        .HasColumnType("bigint");

                    b.Property<int>("amount")
                        .HasColumnType("integer");

                    b.HasKey("order_id", "product_option_id");

                    b.HasIndex("product_option_id");

                    b.ToTable("orders_product_options");
                });

            modelBuilder.Entity("sections_categories", b =>
                {
                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.Property<long>("section_id")
                        .HasColumnType("bigint");

                    b.HasKey("category_id", "section_id");

                    b.HasIndex("section_id");

                    b.ToTable("sections_categories");
                });

            modelBuilder.Entity("ApplicationCore.Entities.CustomerInfo", b =>
                {
                    b.HasOne("ApplicationCore.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("ApplicationCore.Entities.CustomerInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entities.ImageInfo", b =>
                {
                    b.HasOne("ApplicationCore.Entities.ProductColor", "ProductColor")
                        .WithMany("ImagesInfos")
                        .HasForeignKey("ProductColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductColor");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Order", b =>
                {
                    b.HasOne("ApplicationCore.Entities.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Product", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.Subcategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("ApplicationCore.Entities.ProductColor", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Color", "Color")
                        .WithMany("ProductColors")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");
                });

            modelBuilder.Entity("ApplicationCore.Entities.ProductOption", b =>
                {
                    b.HasOne("ApplicationCore.Entities.ProductColor", "ProductColor")
                        .WithMany("ProductOptions")
                        .HasForeignKey("ProductColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.Product", "Product")
                        .WithMany("ProductOptions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductColor");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Review", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("category_subcategories", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.Subcategory", null)
                        .WithMany()
                        .HasForeignKey("subcategory_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("orders_product_options", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.ProductOption", null)
                        .WithMany()
                        .HasForeignKey("product_option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sections_categories", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.Section", null)
                        .WithMany()
                        .HasForeignKey("section_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Color", b =>
                {
                    b.Navigation("ProductColors");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Product", b =>
                {
                    b.Navigation("ProductOptions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ApplicationCore.Entities.ProductColor", b =>
                {
                    b.Navigation("ImagesInfos");

                    b.Navigation("ProductOptions");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Subcategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
