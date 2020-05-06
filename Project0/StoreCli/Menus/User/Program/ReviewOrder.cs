using System;
using Util;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Menu to review and place an order.
    /// </summary>
    class ReviewOrder : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public ReviewOrder(ApplicationData.State appState): base(appState) {
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Review Order");
            this.ApplicationState.UserData.RefreshCurrentOrder();
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
        public void InputLoop()
        {
            if (this.ApplicationState.UserData.CurrentOrderId == null)
            {
                this.AbortThenExit("No current order on file.");
                return;
            }

            var orderTotalCost = 0.0;
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var order = db.GetOrderById(this.ApplicationState.UserData.CurrentOrderId);
                if (order.OrderLineItems.Count == 0)
                {
                    CliInput.PressAnyKey("There are no items in your order.");
                    this.MenuExit();
                    return;
                }
                Console.WriteLine("Qty\tEach\tTotal\tName");
                Console.WriteLine("===\t=====\t=====\t====");


                foreach (var o in order.OrderLineItems)
                {
                    var lineItemTotal = o.Product.Price * o.Quantity;
                    orderTotalCost += lineItemTotal;
                    Console.WriteLine($"{o.Quantity}\t${o.Product.Price}\t${lineItemTotal}\t{o.Product.Name}");
                }
            }
            Console.Write("---\t-----\t-----\t--------------------------\n");
            Console.Write($"\t\t${orderTotalCost}\n\n");
            Console.WriteLine("1.  Place Order");
            Console.WriteLine("2.  Exit");

            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.D1) {
                    Console.WriteLine("Placing order...");

                    var orderId = this.ApplicationState.UserData.CurrentOrderId;
                    try
                    {
                        var orderResult = this.ApplicationState.DbOptions.PlaceOrder(orderId, Util.Config.MAX_ORDER_QUANTITY);
                        switch (orderResult)
                        {
                            case PlaceOrderResult.Ok:
                                this.AbortThenExit("Order placed successfully.");
                                return;
                            case PlaceOrderResult.OutOfStock:
                                this.AbortThenExit("Unable to place order: Some items are out of stock.");
                                return;
                            case PlaceOrderResult.NoLineItems:
                                this.AbortThenExit("Unable to place order: There are no items in the order.");
                                return;
                            case PlaceOrderResult.OrderNotFound:
                                this.AbortThenExit("Unable to place order: Unable to locate the order.");
                                return;
                            case PlaceOrderResult.HighQuantityRejection:
                                this.AbortThenExit("Unable to place order: Item quantities too high.");
                                return;
                        }
                    } catch (NullReferenceException) {
                        this.AbortThenExit("Unable to place order: Order id is missing (this is a bug).");
                        return;
                    }
                }
                else if (cki.Key == ConsoleKey.D2 || cki.Key == ConsoleKey.Enter)
                {
                    this.MenuExit();
                    return;
                }
            } while (true);
        }
    }
}