using Xunit;
using System.Linq;
using System;
using StoreDb;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;

namespace TestStoreDb
{
    public class TestOrder
    {
        private (Customer, Location, Product, LocationInventory) SimplePopulate(StoreContext db)
        {
            var customer = new Customer(Guid.NewGuid().ToString());
            var location = new Location(Guid.NewGuid().ToString());
            var product = new Product(Guid.NewGuid().ToString(), 1.0);
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

            String productName;
            Guid customerId;
            using (var db = new StoreContext(options))
            {
                var (customer, location, product, inventory) = SimplePopulate(db);
                customerId = customer.CustomerId;
                productName = product.Name;

                var order = new Order(customer, location);
                var orderLine = new OrderLineItem(order, product);
                orderLine.Quantity = 8;
                order.OrderLineItems.Add(orderLine);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers where c.CustomerId == customerId select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);
                Assert.Equal(order.OrderLineItems[0].Quantity, 8);

                Assert.Equal(PlaceOrderResult.Ok, db.PlaceOrder(order));

                var inventory = (from i in db.LocationInventories where i.Product.Name == productName select i).First();
                Assert.Equal(2, inventory.Quantity);

            }
        }

        [Fact]
        public void PlacesOrderWithMultipleLineItems()
        {
            var options = TestUtil.GetMemDbOptions("PlacesOrderWithMultipleLineItems");

            Guid product1Id, product2Id;
            Guid customerId;

            using (var db = new StoreContext(options))
            {
                var customer = new Customer(Guid.NewGuid().ToString());
                var location = new Location(Guid.NewGuid().ToString());
                db.Add(customer);
                db.Add(location);
                customerId = customer.CustomerId;

                var product1 = new Product(Guid.NewGuid().ToString(), 1.0);
                var inventory1 = new LocationInventory(product1, location, 10);
                db.Add(product1);
                db.Add(inventory1);
                product1Id = product1.ProductId;

                var product2 = new Product(Guid.NewGuid().ToString(), 1.0);
                var inventory2 = new LocationInventory(product2, location, 20);
                db.Add(product2);
                db.Add(inventory2);
                product2Id = product2.ProductId;

                var order = new Order(customer, location);
                var orderLine1 = new OrderLineItem(order, product1);
                orderLine1.Quantity = 5;
                var orderLine2 = new OrderLineItem(order, product2);
                orderLine2.Quantity = 7;
                order.OrderLineItems.Add(orderLine1);
                order.OrderLineItems.Add(orderLine2);

                db.Add(order);
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                var customer = (from c in db.Customers where c.CustomerId == customerId select c).First();

                var order =
                    (from o in db.Orders
                    where o.Customer.CustomerId == customer.CustomerId
                    select o).First();

                Assert.Equal(order.OrderLineItems.Count(), 2);
                Assert.Equal(PlaceOrderResult.Ok, db.PlaceOrder(order));

                var invProduct1 =
                    (from i in db.LocationInventories where i.Product.ProductId == product1Id select i).First();
                Assert.Equal(5, invProduct1.Quantity);

                var invProduct2 =
                    (from i in db.LocationInventories where i.Product.ProductId == product2Id select i).First();
                Assert.Equal(13, invProduct2.Quantity);
            }
        }

        [Fact]
        public void RejectsOrderWhenNotEnoughInventory()
        {
            var options = TestUtil.GetMemDbOptions("RejectsOrderWhenNotEnoughInventory");

            String product1Name, product2Name;
            Guid customerId;
            using (var db = new StoreContext(options))
            {
                var (customer, location, product1, inventory) = SimplePopulate(db);
                customerId = customer.CustomerId;
                var product2 = new Product(Guid.NewGuid().ToString(), 2.0);
                db.Add(product2);
                var inventory2 = new LocationInventory(product2, location, 20);
                db.Add(inventory2);

                product1Name = product1.Name;
                product2Name = product2.Name;

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
                var customer = (from c in db.Customers where c.CustomerId == customerId select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);

                Assert.Equal(PlaceOrderResult.OutOfStock, db.PlaceOrder(order));
            }

            using (var db = new StoreContext(options))
            {
                var inventoryP1 = (from i in db.LocationInventories where i.Product.Name == product1Name select i).First();
                Assert.Equal(10, inventoryP1.Quantity);

                var inventoryP2 = (from i in db.LocationInventories where i.Product.Name == product2Name select i).First();
                Assert.Equal(20, inventoryP2.Quantity);
            }
        }

        [Fact]
        public void RejectsOrderWithNonExistentInventory()
        {
            var options = TestUtil.GetMemDbOptions("RejectsOrderWithNonExistentInventory");

            String productName;
            Guid customerId;
            using (var db = new StoreContext(options))
            {
                var (customer, location, product1, inventory) = SimplePopulate(db);
                customerId = customer.CustomerId;
                var product2 = new Product(Guid.NewGuid().ToString(), 2.0);
                db.Add(product2);
                productName = product1.Name;

                // Intentionally not adding product 2 to inventory
                // var inventory2 = new LocationInventory(product2, location, 20);
                // db.Add(inventory2);

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
                var customer = (from c in db.Customers where c.CustomerId == customerId select c).First();

                var order  = (from o in db.Orders
                              where o.Customer.CustomerId == customer.CustomerId
                              select o).First();

                Assert.Equal(customer.CustomerId, order.Customer.CustomerId);

                Assert.Equal(PlaceOrderResult.OutOfStock, db.PlaceOrder(order));
            }

            using (var db = new StoreContext(options))
            {
                var inventoryP1 = (from i in db.LocationInventories where i.Product.Name == productName select i).First();
                Assert.Equal(10, inventoryP1.Quantity);
            }
        }
 
    }
}
