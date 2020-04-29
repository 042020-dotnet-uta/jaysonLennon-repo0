using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StoreDb
{
    public static class StoreExtensions 
    {
        public static IQueryable<Customer> FindCustomerByFirstName(this StoreContext ctx, string firstName)
        {
            firstName = firstName.ToLower();
            return from customer in ctx.Customers
                    where customer.FirstName.ToLower().Contains(firstName)
                    select customer;
        }

        public static IQueryable<Customer> FindCustomerByLastName(this StoreContext ctx, string lastName)
        {
            lastName = lastName.ToLower();
            return from customer in ctx.Customers
                    where customer.LastName.ToLower().Contains(lastName)
                    select customer;
        }

        public static IQueryable<Customer> FindCustomerByName(this StoreContext ctx, string name)
        {
            name = name.ToLower();
            return from customer in ctx.Customers
                    where customer.FirstName.ToLower().Contains(name) || customer.LastName.ToLower().Contains(name)
                    select customer;
        }
    }

    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationInventory> LocationInventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

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
            modelBuilder.Entity<PromotionType>()
                .Property(p => p.PromotionTypeDetail)
                .HasConversion(new EnumToStringConverter<PromotionTypeDetail>());
        }
    }
}