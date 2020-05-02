using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreExtensions
{
    using StoreDb;

    public enum PlaceOrderResult
    {
        Ok,
        OutOfStock
    }
    public enum CreateUserAccountResult
    {
        Ok,
        AccountNameExists,
        MissingLogin,
        MissingPassword,
    }

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
                db.Add(location);
                db.SaveChanges();
            }
        }

        public static PlaceOrderResult PlaceOrder(this StoreContext ctx, Order order)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                foreach (var lineItem in order.OrderLineItems)
                {
                    try
                    {
                        var inventory =
                            (from i in ctx.LocationInventories
                            where i.Quantity >= lineItem.Quantity && i.Product.ProductId == lineItem.Product.ProductId
                            select i).First();
                        
                        // Query already determines if there is sufficient quantity,
                        // so we can ignore the return value of this method call.
                        var newInventory = inventory.TryAdjustQuantity(-lineItem.Quantity);
                        ctx.SaveChanges();
                    }
                    // This occurs when the location is out of stock or if the location
                    // does not carry a product needed to fulfill the order.
                    catch (InvalidOperationException e)
                    {
                        transaction.Rollback();
                        return PlaceOrderResult.OutOfStock;
                    }
                }
                transaction.Commit();
            }
            return PlaceOrderResult.Ok;
        }

        public static bool VerifyCredentials(this DbContextOptions<StoreContext> options, string login, string password)
        {
            var hashed = Util.Hash.Sha256(password);
            using (var db = new StoreContext(options))
            {
                var customer =
                    from c in db.Customers
                    where login == c.Login && hashed == c.Password
                    select c;
                return customer.Count() == 1;
            }
        }

        public static CreateUserAccountResult CreateUserAccount(this DbContextOptions<StoreContext> options, Customer customer)
        {
            if (customer.Login == null || customer.Login == "") return CreateUserAccountResult.MissingLogin;
            if (customer.Password == null) return CreateUserAccountResult.MissingPassword;

            using (var db = new StoreContext(options))
            {
                var loginExists = from c in db.Customers where c.Login == customer.Login select c;
                if (loginExists.Count() > 0) return CreateUserAccountResult.AccountNameExists;

                db.Add(customer);
                db.SaveChanges();
                return CreateUserAccountResult.Ok;
            }
        }

        public static bool LoginExists(this DbContextOptions<StoreContext> options, string login)
        {
            using (var db = new StoreContext(options))
            {
                return (from c in db.Customers where c.Login == login select c).Count() > 0;
            }
        }

        public static IQueryable<Location> GetLocations (this StoreContext ctx)
        {
            return from l in ctx.Locations orderby l.Name select l;
        }
    }
}