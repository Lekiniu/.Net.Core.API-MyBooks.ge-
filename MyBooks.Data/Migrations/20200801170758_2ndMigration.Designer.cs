﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBooks.Data.Models;

namespace MyBooks.Data.Migrations
{
    [DbContext(typeof(MyBooksDBContext))]
    [Migration("20200801170758_2ndMigration")]
    partial class _2ndMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyBooks.Data.Models.Addresses", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .HasMaxLength(50);

                    b.Property<int>("UsesId");

                    b.HasKey("AddressId");

                    b.HasIndex("UsesId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Books", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("InStock");

                    b.Property<float>("Price");

                    b.Property<DateTime>("Publication_Date");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.Property<int>("UserId");

                    b.HasKey("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Cart_items", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId");

                    b.Property<int?>("CartId");

                    b.Property<float>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("ItemId");

                    b.HasIndex("BookId");

                    b.HasIndex("CartId");

                    b.ToTable("Cart_Items");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Catalogs", b =>
                {
                    b.Property<int>("CatalogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("CatalogId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Catalogs_Books", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<int>("CatalogId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CatalogId");

                    b.ToTable("Catalogs_Books");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Files", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.Property<int?>("UsesId");

                    b.HasKey("UserId");

                    b.HasIndex("BookId");

                    b.HasIndex("UsesId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Shopping_Carts", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsOrder");

                    b.Property<int>("UserId");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Shopping_Carts");
                });

            modelBuilder.Entity("MyBooks.Data.Models.UserRoles", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password");

                    b.Property<DateTime>("Registration_Date");

                    b.Property<int>("UserRoleId");

                    b.HasKey("UserId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyBooks.Data.Models.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Addresses", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Users", "User")
                        .WithOne("Address")
                        .HasForeignKey("MyBooks.Data.Models.Addresses", "UsesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Data.Models.Books", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Users", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Data.Models.Cart_items", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Books", "Book")
                        .WithMany("Cart_Items")
                        .HasForeignKey("BookId");

                    b.HasOne("MyBooks.Data.Models.Shopping_Carts", "Shop_Carts")
                        .WithMany("Cart_Items")
                        .HasForeignKey("CartId");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Catalogs_Books", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Books", "Book")
                        .WithMany("Catalogs_Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Data.Models.Catalogs", "Catalog")
                        .WithMany("Catalogs_Books")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Data.Models.Files", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Books", "Book")
                        .WithMany("Files")
                        .HasForeignKey("BookId");

                    b.HasOne("MyBooks.Data.Models.Users", "User")
                        .WithMany("Files")
                        .HasForeignKey("UsesId");
                });

            modelBuilder.Entity("MyBooks.Data.Models.Shopping_Carts", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Users", "User")
                        .WithMany("Shop_Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Data.Models.Users", b =>
                {
                    b.HasOne("MyBooks.Data.Models.UserRoles", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Data.Models.WishList", b =>
                {
                    b.HasOne("MyBooks.Data.Models.Books", "Book")
                        .WithMany("WishList")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Data.Models.Users", "User")
                        .WithMany("WishList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
