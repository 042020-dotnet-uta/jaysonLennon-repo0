using System;
using System.Linq;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    class OrderItems : CliMenu, IMenu
    {
        public OrderItems(ApplicationData.State appState): base(appState) {
            appState.RefreshCurrentOrder();

            if (appState.CurrentOrderId == null)
            {
                using (var db = new StoreContext(appState.DbOptions))
                {
                    var customer = db.GetCustomerById(appState.CustomerId);
                    var location = db.GetLocationById(appState.OperatingLocationId);
                    var order = new Order(customer, location);
                    order.Customer = customer;
                    db.Add(order);
                    db.SaveChanges();
                    appState.CurrentOrderId = order.OrderId;
                }
            }
        }

        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Order Items");
        }

        public void InputLoop()
        {
            Console.WriteLine($"order id = {this.ApplicationState.CurrentOrderId}");
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var inventory = db.GetProductsAvailable(this.ApplicationState.OperatingLocationId);
                inventory.Where(i => i.Quantity > 0);

                var i = 1;
                List<Guid> inventoryIds = inventory.Select(i => i.LocationInventoryId).ToList();
                Console.WriteLine("#\tStock\tName");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"{i}.\t{item.Quantity}\t{item.Product.Name}");
                    i += 1;
                }

                bool itemNumberValidator(int num) {
                    if (num > 0 && num <= i) return true;
                    else {
                        CliPrinter.Error($"Please enter an inventory number between 1 and {i}");
                        return false;
                    }
                }

                Console.WriteLine("");

                var itemIndex = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, itemNumberValidator, "Inventory number: ") ?? 0;
                if (itemIndex == 0) {
                    this.AbortThenExit("No item selected - exiting.");
                    return;
                }

                var inventoryId = inventoryIds[itemIndex - 1];
                var maxQuantity = db.GetInventory(inventoryId).Quantity;

                bool quantityValidator(int num) {
                    if (num >= 0 && num <= maxQuantity) return true;
                    else {
                        CliPrinter.Error($"Please enter a quantity between 0 and {maxQuantity}");
                        return false;
                    }
                }

                var orderQuantity = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, quantityValidator, "Quantity: ") ?? 0;
                if (orderQuantity == 0)
                {
                    this.AbortThenExit("No quantity selected - exiting.");
                    return;
                }

                var product = db.GetProductFromInventoryId(inventoryId);
                var order = db.FindCurrentOrder(this.ApplicationState.CustomerId);
                var orderLine = new OrderLineItem(order, product);
                orderLine.Quantity = orderQuantity;
                db.Add(orderLine);
                db.SaveChanges();

                CliInput.PressAnyKey("\nAdded to order.");
                this.MenuExit();
            }
        }
    }
}
