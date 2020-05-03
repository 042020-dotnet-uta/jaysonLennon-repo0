using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreExtensions
{
    using StoreDb;

    public enum PlaceOrderResult
    {
        Ok,
        OutOfStock,
        NoLineItems,
        OrderNotFound,
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

        public static Customer GetCustomerById(this StoreContext ctx, Guid? customerId)
        {
            if (customerId == null) return null;
            var customer = from c in ctx.Customers where c.CustomerId == customerId select c;
            return customer.Count() == 1 ? customer.First() : null;
        }

        public static Customer GetCustomerByLogin(this StoreContext ctx, string login)
        {
            var customer = from c in ctx.Customers where c.Login == login select c;
            return customer.Count() == 1 ? customer.First() : null;
        }

        public static Location GetLocationById(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;
            var location = from l in ctx.Locations where l.LocationId == locationId select l;
            return location.Count() == 1 ? location.First() : null;
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

        public static PlaceOrderResult PlaceOrder(this DbContextOptions<StoreContext> options, Guid? orderId)
        {
            if (orderId == null) throw new NullReferenceException("Missing orderId");
            using (var db = new StoreContext(options))
            {
                var order = db.GetOrderById(orderId);
                if (order == null) return PlaceOrderResult.OrderNotFound;

                if (order.OrderLineItems.Count() == 0) return PlaceOrderResult.NoLineItems;

                var totalOrderPrice = 0.0;

                var location = order.Location;
                using (var transaction = db.Database.BeginTransaction())
                {
                    foreach(var lineItem in order.OrderLineItems)
                    {
                        var locationInventory = db.FindInventoryId(location, lineItem.Product);
                        if (locationInventory == null) return PlaceOrderResult.OutOfStock;
                        var newStockQuantity = locationInventory.TryAdjustQuantity(-lineItem.Quantity);
                        if (newStockQuantity == null)
                        {
                            transaction.Rollback();
                            return PlaceOrderResult.OutOfStock;
                        }

                        var lineItemPrice = lineItem.Quantity * lineItem.Product.Price;
                        totalOrderPrice += lineItemPrice;
                        lineItem.AmountCharged = lineItemPrice;
                        db.SaveChanges();
                    }
                    order.AmountCharged = totalOrderPrice;
                    order.AmountPaid = totalOrderPrice;
                    order.TimeSubmitted = DateTime.Now;
                    db.SaveChanges();
                    transaction.Commit();
                }
                return PlaceOrderResult.Ok;
            }
        }

        public static Nullable<Guid> VerifyCredentials(this DbContextOptions<StoreContext> options, string login, string password)
        {
            var hashed = Util.Hash.Sha256(password);
            using (var db = new StoreContext(options))
            {
                var customer =
                    from c in db.Customers
                    where login == c.Login && hashed == c.Password
                    select c;
                if (customer.Count() == 1) return customer.First().CustomerId;
                return null;
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

        public static IQueryable<Location> GetLocations(this StoreContext ctx)
        {
            return from l in ctx.Locations orderby l.Name select l;
        }

        public static Location GetLocation(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;

            var locations = from l in ctx.Locations where l.LocationId == locationId select l;
            return locations.Count() > 0 ? locations.First() : null;
        }

        public static bool SetDefaultLocation(this DbContextOptions<StoreContext> options, Guid? customerId, Guid? locationId)
        {
            if (customerId == null || locationId == null) return false;

            using (var db = new StoreContext(options))
            {
                var location = db.GetLocationById(locationId);
                var customer = db.GetCustomerById(customerId);
                if (location == null || customer == null) return false;

                customer.DefaultLocation = location;
                db.SaveChanges();
                return true;
            }
        }

        public static Guid? GetDefaultLocation(this DbContextOptions<StoreContext> options, Guid? customerId)
        {
            if (customerId == null) return null;
            using (var db = new StoreContext(options))
            {
                var customer = from c in db.Customers where c.CustomerId == customerId select c;
                if (customer.Count() == 1)
                {
                    var c = customer.First();
                    if (c.DefaultLocation != null) return c.DefaultLocation.LocationId;
                    else return null;
                }
                return null;
            }
        }

        public static IQueryable<LocationInventory> GetProductsAvailable(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;
            return from i in ctx.LocationInventories where i.Location.LocationId == locationId select i;
        }

        public static LocationInventory GetInventory(this StoreContext ctx, Guid? locationInventoryId)
        {
            if (locationInventoryId == null) return null;
            var inventory =
                from i in ctx.LocationInventories
                where i.LocationInventoryId == locationInventoryId
                select i;
            if (inventory.Count() != 1) return null;
            return inventory.First();
        }

        public static Product GetProductById(this StoreContext ctx, Guid? productId)
        {
            if (productId == null) return null;
            var product = from p in ctx.Products where p.ProductId == productId select p;
            if (product.Count() == 1) return product.First();
            return null;
        }

        public static Product GetProductFromInventoryId(this StoreContext ctx, Guid? inventoryId)
        {
            if (inventoryId == null) return null;
            var product =
                from p in ctx.Products
                join li in ctx.LocationInventories on p.ProductId equals li.Product.ProductId
                where li.LocationInventoryId == inventoryId
                select p;
            if (product.Count() == 1) return product.First();
            return null;
        }

        public static Order FindCurrentOrder(this StoreContext ctx, Guid? customerId)
        {
            if (customerId == null) return null;
            var order =
                from o in ctx.Orders
                where o.Customer.CustomerId == customerId
                      && o.TimeCreated != null
                      && o.TimeSubmitted == null
                select o;
            if (order.Count() > 0) return order.First();
            return null;
        }

        public static Order GetOrderById(this StoreContext ctx, Guid? orderId)
        {
            if (orderId == null) return null;
            var order =
                from o in ctx.Orders
                where o.OrderId == orderId
                select o;
            if (order.Count() > 0) return order.First();
            return null;
        }

        public static LocationInventory FindInventoryId(this StoreContext ctx, Location location, Product product)
        {
            if (location == null || product == null) return null;
            var inventory =
                from li in ctx.LocationInventories
                where li.Location.LocationId == location.LocationId
                      && li.Product.ProductId == product.ProductId
                select li;
            if (inventory.Count() > 0) return inventory.First();
            return null;
        }

        public static void AddLineItem(this StoreContext ctx, Order order, Product product, int quantity)
        {
            if (order == null || product == null) return;
            var currentOrderLine = ctx.OrderLineItems
                .Where(li => li.Product.ProductId == product.ProductId && li.Order.OrderId == order.OrderId);
            if (currentOrderLine.Count() > 0)
            {
                var orderLine = currentOrderLine.First().Quantity += quantity;
            } else {
                var orderLine = new OrderLineItem(order, product);
                orderLine.Quantity = quantity;
                ctx.Add(orderLine);
            }
        }

        public static IQueryable<Order> GetOrderHistory(this StoreContext ctx, Guid? customerId)
        {
            if (customerId == null) return null;

            return
                from o in ctx.Orders
                where o.Customer.CustomerId == customerId
                      && o.TimeSubmitted != null
                select o;
        }

        public static IQueryable<OrderLineItem> GetOrderLines(this StoreContext ctx, Guid? orderId)
        {
            if (orderId == null) return null;

            return
                from li in ctx.OrderLineItems
                where li.Order.OrderId == orderId
                select li;
        }
    }
}