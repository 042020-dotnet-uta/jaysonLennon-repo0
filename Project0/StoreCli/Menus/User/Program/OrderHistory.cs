using System;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;
using System.Linq;

namespace StoreCliMenuUser
{
    class OrderHistory : CliMenu, IMenu
    {
        private List<Guid> OrderIds { get; } = new List<Guid>();
        private enum OrderSortKey
        {
            DateAsc,
            DateDesc,
            PriceAsc,
            PriceDesc,
            LocationAsc,
            LocationDesc,
        }
        private OrderSortKey SortKey { set; get; }

        public void DisplayDetail(StoreContext db, Order order)
        {
            var titleString = $"\nOrder placed on {order.TimeSubmitted} at {order.Location.Name} store.";
            Console.WriteLine(titleString);
            Console.Write(new string('=', titleString.Length));
            Console.Write("\n");

            var displayAlignment = "{0,-7}{1,-9}{2,-40}";
            Console.Write(displayAlignment, "Qty", "Charged", "Item");
            Console.Write("\n");
            Console.Write(displayAlignment, "---", "-------", "----------------------------------------");
            Console.Write("\n");

            foreach (var li in order.OrderLineItems)
            {
                Console.WriteLine(displayAlignment,
                    li.Quantity, "$" + li.AmountCharged, li.Product.Name);
            }
            Console.Write(displayAlignment, "---", "-------", "----------------------------------------");
            Console.Write("\n");
            Console.WriteLine("{0,-7}{1,-9}", "Total", "$" + order.AmountCharged);
            Console.WriteLine("{0,-7}{1,-9}", "Paid", "$" + order.AmountPaid);
            CliInput.PressAnyKey();
        }

        public OrderHistory(ApplicationData.State appState) : base(appState)
        {
            this.SortKey = OrderSortKey.DateDesc;
        }

        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Order History");
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var orders = db.GetOrderHistory(this.ApplicationState.CustomerId);
                switch (this.SortKey)
                {
                    case OrderSortKey.DateDesc:
                        orders = orders.OrderByDescending(o => o.TimeSubmitted);
                        break;
                    case OrderSortKey.DateAsc:
                        orders = orders.OrderBy(o => o.TimeSubmitted);
                        break;
                    case OrderSortKey.PriceDesc:
                        orders = orders.OrderByDescending(o => o.AmountCharged);
                        break;
                    case OrderSortKey.PriceAsc:
                        orders = orders.OrderBy(o => o.AmountCharged);
                        break;
                    case OrderSortKey.LocationDesc:
                        orders = orders.OrderByDescending(o => o.Location.Name);
                        break;
                    case OrderSortKey.LocationAsc:
                        orders = orders.OrderBy(o => o.Location.Name);
                        break;
                }
                var historyDisplayAlignment = "{0,-5}{1,-7}{2,-25}{3,-15}";
                Console.WriteLine(historyDisplayAlignment, "Num", "Price", "Date", "Store");
                Console.WriteLine(historyDisplayAlignment, "---", "-----", "----", "-----");
                var i = 1;
                this.OrderIds.Clear();
                foreach (var order in orders)
                {
                    this.OrderIds.Add(order.OrderId);
                    Console.WriteLine(historyDisplayAlignment,
                        i + ".", "$" + order.AmountCharged, order.TimeSubmitted, order.Location.Name);
                    i += 1;
                }
            }
        }

        public void InputLoop()
        {
            var inputOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;
            do
            {
                Console.Write("\n\n");
                var line = CliInput.GetLine(inputOptions, v => true, "Sort by [D]ate / [P]rice / [S]tore\nor enter an order number to view details:");
                switch (line)
                {
                    case "D":
                    case "d":
                        this.SortKey = this.SortKey != OrderSortKey.DateDesc ? OrderSortKey.DateDesc : OrderSortKey.DateAsc;
                        break;
                    case "P":
                    case "p":
                        this.SortKey = this.SortKey != OrderSortKey.PriceDesc ? OrderSortKey.PriceDesc : OrderSortKey.PriceAsc;
                        break;
                    case "S":
                    case "s":
                        this.SortKey = this.SortKey != OrderSortKey.LocationAsc ? OrderSortKey.LocationAsc : OrderSortKey.LocationDesc;
                        break;
                    default:
                        if (line == "" || line == null)
                        {
                            this.MenuExit();
                            CliInput.PressAnyKey();
                            return;
                        }
                        try
                        {
                            var orderNum = Int32.Parse(line);
                            if (orderNum > 0 && orderNum <= this.OrderIds.Count)
                            {
                                using (var db = new StoreContext(this.ApplicationState.DbOptions))
                                {
                                    var order = db.GetOrderById(this.OrderIds[orderNum - 1]);
                                    this.DisplayDetail(db, order);
                                    break;
                                }
                            }
                            else break;
                        }
                        catch {
                            CliPrinter.Error("Invalid order number");
                            continue;
                        }
                }
                this.PrintMenu();
            } while (true);
        }
    }
}

