using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Extension methods for StoreContext and DbContextOptions
/// </summary>
namespace StoreExtensions
{
    using StoreDb;

    /// <summary>
    /// The return value of <c>PlaceOrder()</c>
    /// </summary>
    public enum PlaceOrderResult
    {
        /// <summary>The order was placed successfully.</summary>
        Ok,

        /// <summary>Order rejected because the <c>Location</c> is does not have
        /// enough <c>Product</c> in stock for at least one of the items in the <c>Order</c>.
        /// </summary>
        OutOfStock,

        /// <summary>Order rejected because the order is empty.</summary>
        NoLineItems,

        /// <summary>Order rejected because the order ID does not exist.</summary>
        OrderNotFound,

        /// <summary>Order rejected because the quantity of <c>Product</c> is too high.</summary>
        HighQuantityRejection
    }

    /// <summary>
    /// The return value of <c>CreateUserAccount()</c>
    /// </summary>
    public enum CreateUserAccountResult
    {
        /// <summary>The account was created successfully.</summary>
        Ok,

        /// <summary>Failed to create account: another account with that name already exists.</summary>
        AccountNameExists,

        /// <summary>Failed to create account: no login name was present in <c>Customer</c> object.</summary>
        MissingLogin,

        /// <summary>Failed to create account: no password was present in the <c>Customer</c> object.</summary>
        MissingPassword,
    }

    /// <summary>
    /// Extension methods for StoreContext and DbContextOptions
    /// </summary>
    public static class StoreExtensions
    {
        /// <summary>
        /// Searches for a <c>Customer</c> by their first name.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="firstName">Search for a first name containing this string.</param>
        /// <returns>An <c>IQueryable</c> representing customers that have a first name containing the search string.</returns>
        public static IQueryable<Customer> FindCustomerByFirstName(this StoreContext ctx, string firstName)
        {
            firstName = firstName.ToLower();
            return from customer in ctx.Customers
                   where customer.FirstName.ToLower().Contains(firstName)
                   select customer;
        }

        /// <summary>
        /// Searches for a <c>Customer</c> by their last name.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="lastName">Search for a last name containing this string.</param>
        /// <returns>An <c>IQueryable</c> representing customers that have a last name containing the search string.</returns>
        public static IQueryable<Customer> FindCustomerByLastName(this StoreContext ctx, string lastName)
        {
            lastName = lastName.ToLower();
            return from customer in ctx.Customers
                   where customer.LastName.ToLower().Contains(lastName)
                   select customer;
        }

        /// <summary>
        /// Searches for a <c>Customer</c> by name.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="name">Search for customers that have this search string in either first or last name.</param>
        /// <returns>An <c>IQueryable</c> representing customers that have either a first or last name containing the search string.</returns>
        public static IQueryable<Customer> FindCustomerByName(this StoreContext ctx, string name)
        {
            // TODO: split customer name search when a space is in query.
            name = name.ToLower();
            return from customer in ctx.Customers
                   where customer.FirstName.ToLower().Contains(name) || customer.LastName.ToLower().Contains(name)
                   select customer;
        }

        /// <summary>
        /// Retrieves a <c>Customer</c> object that matches the given customer ID.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The <c>Customer</c> object, or <c>null</c> if not found.</returns>
        public static Customer GetCustomerById(this StoreContext ctx, Guid? customerId)
        {
            if (customerId == null) return null;
            var customer = from c in ctx.Customers where c.CustomerId == customerId select c;
            return customer.Count() == 1 ? customer.First() : null;
        }

        /// <summary>
        /// Retrieves a <c>Customer</c> object that matches the given customer login name.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="customerId">The exact login name for the customer.</param>
        /// <returns>The <c>Customer</c> object, or <c>null</c> if not found.</returns>
        public static Customer GetCustomerByLogin(this StoreContext ctx, string login)
        {
            var customer = from c in ctx.Customers where c.Login == login select c;
            return customer.Count() == 1 ? customer.First() : null;
        }

        /// <summary>
        /// Retrieves a <c>Location</c> object based on the location ID.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="locationId">The ID of the <c>Location</c>.</param>
        /// <returns>The <c>Location</c> object, or <c>null</c> if not found.</returns>
        public static Location GetLocationById(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;
            var location = from l in ctx.Locations where l.LocationId == locationId select l;
            return location.Count() == 1 ? location.First() : null;
        }

        /// <summary>
        /// Adds a new <c>Location</c> to the database.
        /// </summary>
        /// <param name="options">Options for creating a new <c>StoreContext</c>.</param>
        /// <param name="location">The <c>Location</c> object to be saved.</param>
        public static void AddLocation(this DbContextOptions<StoreContext> options, Location location)
        {
            // TODO: disallow duplicate location names.
            using (var db = new StoreContext(options))
            {
                db.Add(location);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Places a new <c>Order</c>.
        /// </summary>
        /// <param name="options">Options for creating a new <c>StoreContext</c>.</param>
        /// <param name="orderId">The ID of a previously saved <c>Order</c>.</param>
        /// <param name="maxQuantity">The maximum amount of <c>Product</c> that may be present in an <c>Order</c>.</param>
        /// <returns>A <c>PlaceOrderResult</c> detailing whether the operation completed successfully.</returns>
        public static PlaceOrderResult PlaceOrder(this DbContextOptions<StoreContext> options, Guid? orderId, int maxQuantity)
        {
            if (orderId == null) throw new NullReferenceException("Missing orderId");
            using (var db = new StoreContext(options))
            {
                var order = db.GetOrderById(orderId);
                if (order == null) return PlaceOrderResult.OrderNotFound;

                if (order.OrderLineItems.Count() == 0) return PlaceOrderResult.NoLineItems;

                var totalOrderPrice = 0.0;
                var totalItemQuantity = 0;

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
                        totalItemQuantity += lineItem.Quantity;
                        db.SaveChanges();
                    }
                    if (totalItemQuantity > maxQuantity) return PlaceOrderResult.HighQuantityRejection;
                    order.AmountPaid = totalOrderPrice;
                    order.TimeSubmitted = DateTime.Now;
                    db.SaveChanges();
                    transaction.Commit();
                }
                return PlaceOrderResult.Ok;
            }
        }

        /// <summary>
        /// Retrieves the total amount charged for an <c>Order</c>.
        /// </summary>
        /// <param name="ctx">A <c>StoreContext</c> object.</param>
        /// <param name="order">The <c>Order</c> that the charges should be calculated from.</param>
        /// <returns>A <c>double</c> representing the amount charged for the <c>Order</c>,
        /// or <c>null</c> if one of the line items is missing a charge amount.</returns>
        public static Nullable<double> GetAmountCharged(this StoreContext ctx, Order order)
        {
            if (order == null) return null;
            double totalCharged = 0.0;
            foreach (var li in ctx.OrderLineItems.Where(li => li.Order.OrderId == order.OrderId))
            {
                if (li.AmountCharged == null) return null;
                totalCharged += li.AmountCharged ?? 0.0;
            }
            return totalCharged;
        }

        /// <summary>
        /// Attempts to retrieve the ID of a customer based on the provided credentials.
        /// </summary>
        /// <param name="options">Options for creating a new context.</param>
        /// <param name="login">The login name of the customer.</param>
        /// <param name="password">The password provided by the customer.</param>
        /// <returns>The ID of the customer if the login and password match; otherwise returns <c>null</c>.</returns>
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

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="options">Options for creating a new context.</param>
        /// <param name="customer">The <c>Customer</c> object to be saved.</param>
        /// <returns>A <c>CreateUserAccountResult</c> detailing whether this operation succeeded or failed.</returns>
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

        /// <summary>
        /// Determines whether the provided login name exists in the database.
        /// </summary>
        /// <param name="options">Options for creating a new context.</param>
        /// <param name="login">The login name to check.</param>
        /// <returns>Whether the login name exists.</returns>
        public static bool LoginExists(this DbContextOptions<StoreContext> options, string login)
        {
            using (var db = new StoreContext(options))
            {
                return (from c in db.Customers where c.Login == login select c).Count() > 0;
            }
        }

        /// <summary>
        /// Gets all the locations in the database.
        /// </summary>
        /// <param name="ctx">Store context options.</param>
        /// <returns>An <c>IQueryable</c> representing all <c>Location</c> objects in the database.</returns>
        public static IQueryable<Location> GetLocations(this StoreContext ctx)
        {
            return from l in ctx.Locations orderby l.Name select l;
        }

        /// <summary>
        /// Gets a specific <c>Location</c> from the location ID.
        /// </summary>
        /// <param name="ctx">Store context options.</param>
        /// <param name="locationId">The location ID to locate.</param>
        /// <returns>The <c>Location</c> with the specified ID, or <c>null</c> if not found.</returns>
        public static Location GetLocation(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;

            var locations = from l in ctx.Locations where l.LocationId == locationId select l;
            return locations.Count() > 0 ? locations.First() : null;
        }

        /// <summary>
        /// Sets the default location to be used by a customer.
        /// </summary>
        /// <param name="options">Options for creating a new context.</param>
        /// <param name="customerId">The ID of the customer to operate upon.</param>
        /// <param name="locationId">The location ID to set as the default.</param>
        /// <returns>Whether the operation succeeded.</returns>
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

        /// <summary>
        /// Retrieves the ID of the default location for a customer (if one exists).
        /// </summary>
        /// <param name="options">Options for creating a new context.</param>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A <c>Guid</c> corresponding to the Location ID of the default location,
        /// or <c>null</c> if no default is set.</returns>
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

        /// <summary>
        /// Gets all products that a <c>Location</c> carries, even if none are currently in stock.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="locationId">The ID of the <c>Location</c> to query.</param>
        /// <returns>An <c>IQueryable</c> representing inventory statuses for all inventory items at the given <c>Location</c>.</returns>
        public static IQueryable<LocationInventory> GetProductsAvailable(this StoreContext ctx, Guid? locationId)
        {
            if (locationId == null) return null;
            return from i in ctx.LocationInventories where i.Location.LocationId == locationId select i;
        }

        /// <summary>
        /// Gets the inventory of a specific item at a specific <c>Location</c>.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="locationInventoryId">The ID of the <c>LocationInventory</c> to be queried.</param>
        /// <returns>A <c>LocationInventory</c>, or <c>null</c> if not found.</returns>
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

        /// <summary>
        /// Retrieves a <c>Product</c> by it's ID number.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="productId">The ID of the <c>Product</c>.</param>
        /// <returns>A <c>Product</c> object if found, <c>null</c> otherwise.</returns>
        public static Product GetProductById(this StoreContext ctx, Guid? productId)
        {
            if (productId == null) return null;
            var product = from p in ctx.Products where p.ProductId == productId select p;
            if (product.Count() == 1) return product.First();
            return null;
        }

        /// <summary>
        /// Gets a <c>Product</c> based on its <c>LocationInventory</c> ID.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="inventoryId">The <c>LocationInventory</c> ID to be queried.</param>
        /// <returns>A <c>Product</c> object if found, <c>null</c> otherwise.</returns>
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

        /// <summary>
        /// Gets the current open <c>Order</c> for a customer (if any).
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="customerId">The ID of the customer to query.</param>
        /// <returns>The current open <c>Order</c> for the customer, or <c>null</c>
        /// if the customer has no open orders.</returns>
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

        /// <summary>
        /// Retrieves an Order based on it's ID.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="orderId">The ID of the <c>Order</c> to be queried.</param>
        /// <returns>The <c>Order</c> if found, or <c>null</c> if not found.</returns>
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

        /// <summary>
        /// Retrieves the LocationInventory for a <c>Product</c> based on its <c>Location</c>.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="location"><c>Location</c> to check.</param>
        /// <param name="product"><c>Product</c> to check.</param>
        /// <returns>The <c>LocationInventory</c> object for the given product at the given
        /// location, or <c>null</c> if not found. A null return value indicates that the given
        /// location does not carry the <c>Product</c>.</returns>
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

        /// <summary>
        /// Adds a new <c>OrderLineItem</c> to the given <c>Order</c> with the given <c>Product</c> and quantity.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="order">The <c>Order</c> that the <c>OrderLineItem</c> will be added to.</param>
        /// <param name="product">The <c>Product</c> that is being added to the <c>OrderLineItem</c>.</param>
        /// <param name="quantity">The quantity of <c>Product</c> to add to the <c>OrderLineItem</c>.</param>
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

        /// <summary>
        /// Retrieves the Orders for a specific customer.
        /// Only submitted orders will be returned.
        /// Use <c>FindCurrentOrder()</c> to get any currently opened order.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="customerId">The ID of the customer to query.</param>
        /// <returns>An <c>IQueryable</c> of <c>Order</c> submitted by the customer.</returns>
        public static IQueryable<Order> GetOrderHistory(this StoreContext ctx, Guid? customerId)
        {
            if (customerId == null) return null;

            return
                from o in ctx.Orders
                where o.Customer.CustomerId == customerId
                      && o.TimeSubmitted != null
                select o;
        }

        /// <summary>
        /// Retrieves the <c>Orders</c> for a specific <c>Location</c>.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="location">The <c>Location</c> to query.</param>
        /// <returns>An <c>IQueryable</c> of <c>Order</c> for this specific location.</returns>
        public static IQueryable<Order> GetOrderHistory(this StoreContext ctx, Location location)
        {
            if (location == null) return null;
            return
                from o in ctx.Orders
                where o.Location.LocationId == location.LocationId
                select o;
        }

        /// <summary>
        /// Retrieves the <c>OrderLineItems</c> of a specific <c>Order</c>.
        /// </summary>
        /// <param name="ctx">Store context object.</param>
        /// <param name="orderId">The ID of the <c>Order</c> to query.</param>
        /// <returns>An <c>IQueryable</c> of <c>OrderLineItem</c> for this specific <c>Order</c>.</returns>
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