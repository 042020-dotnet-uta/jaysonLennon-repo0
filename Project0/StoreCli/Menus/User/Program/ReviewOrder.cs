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

            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var order = db.GetOrderById(this.ApplicationState.CurrentOrderId);
                Console.WriteLine("Qty\tUCost\tTCost\tName");
                Console.WriteLine("===\t=====\t=====\t====");
                foreach (var o in order.OrderLineItems)
                {
                    Console.WriteLine($"{o.Quantity}\t${o.Product.Price}\t${o.Product.Price * o.Quantity}\t{o.Product.Name}");
                }
            }
            this.AbortThenExit("");
        }
    }
}

