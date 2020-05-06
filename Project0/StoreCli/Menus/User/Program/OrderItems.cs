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
        private List<Guid> InventoryIds = new List<Guid>();

        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public OrderItems(ApplicationData.State appState): base(appState) {
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Order Items");
            this.ApplicationState.UserData.RefreshCurrentOrder();

            if (this.ApplicationState.UserData.CurrentOrderId == null)
            {
                using (var db = new StoreContext(this.ApplicationState.DbOptions))
                {
                    var customer = db.GetCustomerById(this.ApplicationState.UserData.CustomerId);
                    var location = db.GetLocationById(this.ApplicationState.UserData.OperatingLocationId);
                    var order = new Order(customer, location);
                    order.Customer = customer;
                    db.Add(order);
                    db.SaveChanges();
                    this.ApplicationState.UserData.CurrentOrderId = order.OrderId;
                }
            }

            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var inventory = db.GetProductsAvailable(this.ApplicationState.UserData.OperatingLocationId);
                inventory = inventory.Where(i => i.Quantity > 0);

                var order = db.GetOrderById(this.ApplicationState.UserData.CurrentOrderId);

                var i = 1;
                this.InventoryIds = inventory.Select(i => i.LocationInventoryId).ToList();
                Console.WriteLine("#\tStock\tName");
                foreach (var stock in inventory)
                {
                    var projectedQuantity = db.ProjectStockBasedOnOrder(order, stock.Product);
                    if (projectedQuantity < 0) projectedQuantity = 0;
                    Console.WriteLine($"{i}.\t{projectedQuantity}\t{stock.Product.Name}");
                    i += 1;
                }
                Console.Write("\n----------------------------------------------\n");
            }
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        public void InputLoop()
        {
            do
            {
                PrintMenu();
                using (var db = new StoreContext(this.ApplicationState.DbOptions))
                {
                    var order = db.FindCurrentOrder(this.ApplicationState.UserData.CustomerId);
                    if (order == null)
                    {
                        CliPrinter.Error("There was an error retrieving your order. Please try again.");
                        break;
                    }

                    bool itemNumberValidator(int num) {
                        if (num > 0 && num <= this.InventoryIds.Count) return true;
                        else {
                            CliPrinter.Error($"Please enter an inventory number between 1 and {this.InventoryIds.Count}");
                            return false;
                        }
                    }

                    var itemIndex = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, itemNumberValidator, "Inventory number: ") ?? 0;
                    if (itemIndex == 0) break;

                    var inventoryId = this.InventoryIds[itemIndex - 1];
                    var product = db.GetProductFromInventoryId(inventoryId);

                    var projectedQuantity = db.ProjectStockBasedOnOrder(order, product);

                    if (projectedQuantity <= 0)
                    {
                        CliInput.PressAnyKey("That item is out of stock");
                        continue;
                    }

                    var orderQuantity = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty,
                                                        v => v > 0 && v <= projectedQuantity,
                                                        $"Quantity [1-{projectedQuantity}]: ") ?? 0;
                    if (orderQuantity == 0) continue;

                    db.AddLineItem(order, product, orderQuantity);
                    db.SaveChanges();

                    CliInput.PressAnyKey("\nAdded to order.");
                }
            } while (true);
            this.MenuExit();
        }
    }
}
