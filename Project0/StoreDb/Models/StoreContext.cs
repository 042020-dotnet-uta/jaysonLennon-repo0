using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StoreDb
{

    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationInventory> LocationInventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }

        public StoreContext() { }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options
                    .UseSqlite("Data Source=store.sqlite")
                    .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Order>().Property(t => t.TimeCreated).IsRequired(false);
        }
    }
}