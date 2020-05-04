using System;
using System.Linq;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Menu to place an order.
    /// </summary>
    class OrderItems : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public OrderItems(ApplicationData.State appState): base(appState) {
            appState.UserData.RefreshCurrentOrder();

            if (appState.UserData.CurrentOrderId == null)
            {
                using (var db = new StoreContext(appState.DbOptions))
                {
                    var customer = db.GetCustomerById(appState.UserData.CustomerId);
                    var location = db.GetLocationById(appState.UserData.OperatingLocationId);
                    var order = new Order(customer, location);
                    order.Customer = customer;
                    db.Add(order);
                    db.SaveChanges();
                    appState.UserData.CurrentOrderId = order.OrderId;
                }
            }
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Order Items");
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        public void InputLoop()
        {
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var inventory = db.GetProductsAvailable(this.ApplicationState.UserData.OperatingLocationId);
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

                Console.Write("\n----------------------------------------------\n");

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
                var order = db.FindCurrentOrder(this.ApplicationState.UserData.CustomerId);
                db.AddLineItem(order, product, orderQuantity);
                db.SaveChanges();

                CliInput.PressAnyKey("\nAdded to order.");
                this.MenuExit();
            }
        }
    }
}
