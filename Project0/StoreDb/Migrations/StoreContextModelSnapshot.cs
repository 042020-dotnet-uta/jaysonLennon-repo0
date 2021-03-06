﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreDb;

namespace StoreDb.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("StoreDb.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Line1AddressLine1Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Line2AddressLine2Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StateId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ZipCodeId")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressId");

                    b.HasIndex("CityId");

                    b.HasIndex("Line1AddressLine1Id");

                    b.HasIndex("Line2AddressLine2Id");

                    b.HasIndex("StateId");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("StoreDb.AddressLine1", b =>
                {
                    b.Property<Guid>("AddressLine1Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressLine1Id");

                    b.ToTable("AddressLine1");
                });

            modelBuilder.Entity("StoreDb.AddressLine2", b =>
                {
                    b.Property<Guid>("AddressLine2Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressLine2Id");

                    b.ToTable("AddressLine2");
                });

            modelBuilder.Entity("StoreDb.City", b =>
                {
                    b.Property<Guid>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("StoreDb.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DefaultLocationLocationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.HasIndex("AddressId");

                    b.HasIndex("DefaultLocationLocationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("StoreDb.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("LocationId");

                    b.HasIndex("AddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("StoreDb.LocationInventory", b =>
                {
                    b.Property<Guid>("LocationInventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("LocationInventoryId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("LocationInventories");
                });

            modelBuilder.Entity("StoreDb.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double?>("AmountPaid")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeFulfilled")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeSubmitted")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StoreDb.OrderLineItem", b =>
                {
                    b.Property<Guid>("OrderLineItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double?>("AmountCharged")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderLineItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLineItems");
                });

            modelBuilder.Entity("StoreDb.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StoreDb.State", b =>
                {
                    b.Property<Guid>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("StateId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("StoreDb.ZipCode", b =>
                {
                    b.Property<Guid>("ZipCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .HasColumnType("TEXT");

                    b.HasKey("ZipCodeId");

                    b.ToTable("ZipCode");
                });

            modelBuilder.Entity("StoreDb.Address", b =>
                {
                    b.HasOne("StoreDb.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("StoreDb.AddressLine1", "Line1")
                        .WithMany()
                        .HasForeignKey("Line1AddressLine1Id");

                    b.HasOne("StoreDb.AddressLine2", "Line2")
                        .WithMany()
                        .HasForeignKey("Line2AddressLine2Id");

                    b.HasOne("StoreDb.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("StoreDb.ZipCode", "Zip")
                        .WithMany()
                        .HasForeignKey("ZipCodeId");
                });

            modelBuilder.Entity("StoreDb.Customer", b =>
                {
                    b.HasOne("StoreDb.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("StoreDb.Location", "DefaultLocation")
                        .WithMany()
                        .HasForeignKey("DefaultLocationLocationId");
                });

            modelBuilder.Entity("StoreDb.Location", b =>
                {
                    b.HasOne("StoreDb.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("StoreDb.LocationInventory", b =>
                {
                    b.HasOne("StoreDb.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("StoreDb.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("StoreDb.Order", b =>
                {
                    b.HasOne("StoreDb.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("StoreDb.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("StoreDb.OrderLineItem", b =>
                {
                    b.HasOne("StoreDb.Order", "Order")
                        .WithMany("OrderLineItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("StoreDb.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
