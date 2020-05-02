using Xunit;
using System.Linq;
using StoreDb;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;

namespace TestStoreDb
{
    public class TestOrder
    {
        private (Customer, Location, Product, LocationInventory) SimplePopulate(StoreContext db)
        {
            var customer = new Customer("customer");
            var location = new Location("location");
            var product = new Product("product", 1.0);
            var inventory = new LocationInventory(product, location, 10);
            db.Add(customer);
            db.Add(location);
            db.Add(product);
            db.Add(inventory);

            return (customer, location, product, inventory);
        }

        [Fact]
        public void PlacesOrderWithSingleLineItem()
        {
            var options = TestUtil.GetMemDbOptions("PlacesOrderWithSingleLineItem");
            using (var db = new StoreContext(options))
            {
                var (customer, location, product, inventory) = SimplePopulate(db);

                var order = new Order(customer, location);
                var orderLine = new OrderLineItem(order, product);
                orderLine.Quantity = 8;
                order.OrderLineItems.Add(orderLine);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);
                Assert.Equal(order.OrderLineItems[0].Quantity, 8);

                Assert.Equal(PlaceOrderResult.Ok, db.PlaceOrder(order));

                var inventory = (from i in db.LocationInventories where i.Product.Name == "product" select i).First();
                Assert.Equal(2, inventory.Quantity);

            }
        }

        [Fact]
        public void PlacesOrderWithMultipleLineItem()
        {
            var options = TestUtil.GetMemDbOptions("PlacesOrderWithMultipleLineItems");
            using (var db = new StoreContext(options))
            {
                var (customer, location, product1, inventory) = SimplePopulate(db);
                var product2 = new Product("product2", 2.0);
                db.Add(product2);
                var inventory2 = new LocationInventory(product2, location, 20);
                db.Add(inventory2);

                var order = new Order(customer, location);

                var orderLine1 = new OrderLineItem(order, product1);
                orderLine1.Quantity = 8;
                order.OrderLineItems.Add(orderLine1);

                var orderLine2 = new OrderLineItem(order, product2);
                orderLine2.Quantity = 10;
                order.OrderLineItems.Add(orderLine2);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);
                Assert.Equal(order.OrderLineItems.Count(), 2);

                foreach (var lineItem in order.OrderLineItems)
                {
                    if (lineItem.Product.Name == "product") Assert.Equal(8, lineItem.Quantity);
                    if (lineItem.Product.Name == "product2") Assert.Equal(10, lineItem.Quantity);
                }

                Assert.Equal(PlaceOrderResult.Ok, db.PlaceOrder(order));

                var inventoryP1 = (from i in db.LocationInventories where i.Product.Name == "product" select i).First();
                Assert.Equal(2, inventoryP1.Quantity);

                var inventoryP2 = (from i in db.LocationInventories where i.Product.Name == "product2" select i).First();
                Assert.Equal(10, inventoryP2.Quantity);
            }
        }

        [Fact]
        public void RejectsOrderWhenNotEnoughInventory()
        {
            var options = TestUtil.GetMemDbOptions("RejectsOrderWhenNotEnoughInventory");
            using (var db = new StoreContext(options))
            {
                var (customer, location, product1, inventory) = SimplePopulate(db);
                var product2 = new Product("product2", 2.0);
                db.Add(product2);
                var inventory2 = new LocationInventory(product2, location, 20);
                db.Add(inventory2);

                var order = new Order(customer, location);

                var orderLine1 = new OrderLineItem(order, product1);
                orderLine1.Quantity = 9;
                order.OrderLineItems.Add(orderLine1);

                var orderLine2 = new OrderLineItem(order, product2);
                orderLine2.Quantity = 21;
                order.OrderLineItems.Add(orderLine2);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);

                Assert.Equal(PlaceOrderResult.OutOfStock, db.PlaceOrder(order));
            }

            using (var db = new StoreContext(options))
            {
                var inventoryP1 = (from i in db.LocationInventories where i.Product.Name == "product" select i).First();
                Assert.Equal(10, inventoryP1.Quantity);

                var inventoryP2 = (from i in db.LocationInventories where i.Product.Name == "product2" select i).First();
                Assert.Equal(20, inventoryP2.Quantity);
            }
        }

        [Fact]
        public void RejectsOrderWithNonExistentInventory()
        {
            var options = TestUtil.GetMemDbOptions("RejectsOrderWithNonExistentInventory");
            using (var db = new StoreContext(options))
            {
                var (customer, location, product1, inventory) = SimplePopulate(db);
                var product2 = new Product("product2", 2.0);
                db.Add(product2);
                // Intentionally not adding product 2 to inventory

                var order = new Order(customer, location);

                var orderLine1 = new OrderLineItem(order, product1);
                orderLine1.Quantity = 9;
                order.OrderLineItems.Add(orderLine1);

                var orderLine2 = new OrderLineItem(order, product2);
                orderLine2.Quantity = 21;
                order.OrderLineItems.Add(orderLine2);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);

                Assert.Equal(PlaceOrderResult.OutOfStock, db.PlaceOrder(order));
            }

            using (var db = new StoreContext(options))
            {
                var inventoryP1 = (from i in db.LocationInventories where i.Product.Name == "product" select i).First();
                Assert.Equal(10, inventoryP1.Quantity);
            }
        }
 
    }
}
