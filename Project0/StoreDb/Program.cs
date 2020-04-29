using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StoreContext())
            {

                var p = new Product("product2");
                Console.WriteLine("Inserting a new product");
                db.Add(p);
                db.SaveChanges();

                p.Detail.Name = "update name prior to query";
                db.SaveChanges();

                Console.WriteLine("Querying for a product");
                var product = db.Products
                    .OrderBy(b => b.ProductId)
                    .First();


                //Console.WriteLine("update");
                //product.Detail = new ProductDetail { Name = "product name" };
                //db.SaveChanges();

                Console.WriteLine($"product id = {product.ProductId}");
                Console.WriteLine($"product detail = {product.Detail}");
                Console.WriteLine($"product name = {product.Detail.Name}");

                var product2 = db.Products
                    .OrderBy(b => b.ProductId)
                    .First();

                Console.WriteLine($"product id = {product2.ProductId}");
                Console.WriteLine($"product2 detail = {product2.Detail}");
                Console.WriteLine($"product2 name = {product2.Detail.Name}");

                product2.Detail.Name = "new name";
                db.SaveChanges();

                var product3 = db.Products
                    .OrderBy(b => b.ProductId)
                    .First();

                Console.WriteLine($"product id = {product3.ProductId}");
                Console.WriteLine($"product3 detail = {product3.Detail}");
                Console.WriteLine($"product3 name = {product3.Detail.Name}");

                var loc = new Location();
                loc.Name = "location 1";
                db.Add(loc);
                db.SaveChanges();
                loc.Address = "address 1";
                db.SaveChanges();

                var locinfo = db.Locations
                    .OrderBy(b => b.LocationId)
                    .First();
                
                Console.WriteLine($"location id = {locinfo.LocationId}");
                Console.WriteLine($"location name = {locinfo.Name}");
                Console.WriteLine($"location address = {locinfo.Address}");
                Console.WriteLine($"location inventory = {locinfo.Inventory}");

                loc.Inventory.Add(new LocationInventory(product2, loc));
                db.SaveChanges();
                var locinfo2 = db.Locations
                    .OrderBy(b => b.LocationId)
                    .First();
                
                Console.WriteLine($"location id = {locinfo2.LocationId}");
                Console.WriteLine($"location name = {locinfo2.Name}");
                Console.WriteLine($"location address = {locinfo2.Address}");
                Console.WriteLine("location inventory list:");
                foreach(var pro in locinfo2.Inventory)
                {
                    Console.WriteLine($"Product name = {pro.Product.Detail.Name}");
                    Console.WriteLine($"Product quantity = {pro.Quantity}");
                    pro.Product.Detail.Name = "renamed from location inventory";
                }
                db.SaveChanges();

                var product4 = db.Products
                    .OrderBy(b => b.ProductId)
                    .First();
                Console.WriteLine($"product id = {product4.ProductId}");
                Console.WriteLine($"product4 detail = {product4.Detail}");
                Console.WriteLine($"product4 name = {product4.Detail.Name}");

                var customer = new Customer("customer 1");
                customer.FirstName = "customer first name";
                db.Add(customer);
                customer.DefaultLocation = loc;
                db.SaveChanges();

                var customer1 = db.Customers
                    .OrderBy(b => b.CustomerId)
                    .First();
                Console.WriteLine($"customer id = {customer1.CustomerId}");
                Console.WriteLine($"customer first name = {customer1.FirstName}");
                Console.WriteLine($"customer default location = {customer1.DefaultLocation}");
                Console.WriteLine($"customer default location name = {customer1.DefaultLocation.Name}");

                customer1.DefaultLocation.Name = "changed location name through customer";
                db.SaveChanges();
                var customer2 = db.Customers
                    .OrderBy(b => b.CustomerId)
                    .First();
                Console.WriteLine($"customer default location name = {customer2.DefaultLocation.Name}");
            }
        }
    }
}