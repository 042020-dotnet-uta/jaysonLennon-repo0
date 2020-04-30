using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StoreExtensions
{
    using StoreDb;
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

        public static void AddCustomer(this DbContextOptions<StoreContext> options, Customer customer)
        {
            using (var db = new StoreContext(options))
            {
                db.Add(customer);
                db.SaveChanges();
            }
        }

        public static void AddLocation(this DbContextOptions<StoreContext> options, Location location)
        {
            using (var db = new StoreContext(options))
            {
                db.AddLocation(location);
            }
        }

        public static void AddLocation(this StoreContext ctx, Location location)
        {
            ctx.Add(location);
            ctx.SaveChanges();
        }
    }
}