using System;
using System.Linq;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    class ReviewOrder : CliMenu, IMenu
    {
        public ReviewOrder(ApplicationData.State appState): base(appState) {
            appState.RefreshCurrentOrder();
        }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Review Order");
        }

        public void InputLoop()
        {
            if (this.ApplicationState.CurrentOrderId == null)
            {
                this.AbortThenExit("No current order on file.");
                return;
            }

            var orderTotalCost = 0.0;
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var order = db.GetOrderById(this.ApplicationState.CurrentOrderId);
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

                    var orderId = this.ApplicationState.CurrentOrderId;
                    try
                    {
                        var orderResult = this.ApplicationState.DbOptions.PlaceOrder(orderId);
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

