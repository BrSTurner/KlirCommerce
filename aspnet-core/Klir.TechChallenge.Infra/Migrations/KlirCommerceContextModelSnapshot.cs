﻿// <auto-generated />
using System;
using Klir.TechChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Klir.TechChallenge.Infra.Migrations
{
    [DbContext(typeof(KlirCommerceContext))]
    partial class KlirCommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Klir.TechChallenge.Domain.Products.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PromotionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f1d73c3-19bb-4948-aa60-a5be7f0cba30"),
                            IsActive = true,
                            Name = "Product A",
                            Price = 20.00m,
                            PromotionId = new Guid("fa8c3e23-7e44-4f53-aebc-7c5f3be2e197")
                        },
                        new
                        {
                            Id = new Guid("6143e542-feec-428f-97d8-df01587add9f"),
                            IsActive = true,
                            Name = "Product B",
                            Price = 4.00m,
                            PromotionId = new Guid("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935")
                        },
                        new
                        {
                            Id = new Guid("822fddcf-5859-47f5-a07c-ac468e633a19"),
                            IsActive = true,
                            Name = "Product C",
                            Price = 2.00m
                        },
                        new
                        {
                            Id = new Guid("1e738d23-fb86-41ba-b0b7-3a4b03883ce6"),
                            IsActive = true,
                            Name = "Product D",
                            Price = 4.00m,
                            PromotionId = new Guid("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935")
                        });
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.Promotions.Models.Promotion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequiredQuantity")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Promotions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa8c3e23-7e44-4f53-aebc-7c5f3be2e197"),
                            Description = "Get 2 for the price of 1.",
                            IsActive = true,
                            Name = "Buy 1 Get 1 Free",
                            RequiredQuantity = 2,
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935"),
                            Description = "Buy 3 products for only 10 Euro.",
                            IsActive = true,
                            Name = "3 for 10 Euro",
                            RequiredQuantity = 3,
                            Type = 2
                        });
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.ShoppingCart.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCart", (string)null);
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.ShoppingCart.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPromotionApplied")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.Products.Models.Product", b =>
                {
                    b.HasOne("Klir.TechChallenge.Domain.Promotions.Models.Promotion", "Promotion")
                        .WithMany("Products")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.ShoppingCart.Models.CartItem", b =>
                {
                    b.HasOne("Klir.TechChallenge.Domain.ShoppingCart.Models.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Klir.TechChallenge.Domain.Products.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.Products.Models.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.Promotions.Models.Promotion", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Klir.TechChallenge.Domain.ShoppingCart.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}