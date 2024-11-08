﻿// <auto-generated />
using System;
using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.Grpc.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241108183110_UpdateExpiryDateProperty")]
    partial class UpdateExpiryDateProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Discount.Grpc.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Discount.Grpc.Models.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Discount.Grpc.Models.SaleEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SalePercent")
                        .HasPrecision(5, 2)
                        .HasColumnType("REAL");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("SaleEvents");
                });

            modelBuilder.Entity("Discount.Grpc.Models.SaleEventProduct", b =>
                {
                    b.Property<int>("SaleEventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("SaleEventId", "ProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleEventId");

                    b.ToTable("SaleEventProducts");
                });

            modelBuilder.Entity("Discount.Grpc.Models.SaleEventProduct", b =>
                {
                    b.HasOne("Discount.Grpc.Models.Product", "Product")
                        .WithMany("SaleEventProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discount.Grpc.Models.SaleEvent", "SaleEvent")
                        .WithMany("SaleEventProducts")
                        .HasForeignKey("SaleEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SaleEvent");
                });

            modelBuilder.Entity("Discount.Grpc.Models.Product", b =>
                {
                    b.Navigation("SaleEventProducts");
                });

            modelBuilder.Entity("Discount.Grpc.Models.SaleEvent", b =>
                {
                    b.Navigation("SaleEventProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
