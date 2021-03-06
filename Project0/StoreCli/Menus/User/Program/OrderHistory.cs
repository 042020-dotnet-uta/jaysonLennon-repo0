using System;
using System.Collections.Generic;
using StoreCliUtil;
using StoreExtensions;
using StoreDb;
using System.Linq;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Menu to view order history.
    /// </summary>
    class OrderHistory : CliMenu, IMenu
    {
        /// <summary>
        /// <c>Order</c> ids as listed in the UI.
        /// </summary>
        private List<Guid> OrderIds { get; set; } = new List<Guid>();

        /// <summary>
        /// How to sort the orders in the UI.
        /// </summary>
        private enum OrderSortKey
        {
            /// <summary>Sort by Date Ascending.</summary>
            DateAsc,
            /// <summary>Sort by Date Descending.</summary>
            DateDesc,
            /// <summary>Sort by Price Ascending.</summary>
            PriceAsc,
            /// <summary>Sort by Price Descending.</summary>
            PriceDesc,
            /// <summary>Sort by Location Ascending.</summary>
            LocationAsc,
            /// <summary>Sort by Location Descending.</summary>
            LocationDesc,
        }

        /// <summary>
        /// Current sort key.
        /// </summary>
        private OrderSortKey SortKey { set; get; } = OrderSortKey.DateDesc;

        /// <summary>
        /// Print out detailed information about an order.
        /// </summary>
        /// <param name="db">Store context.</param>
        /// <param name="order">The <c>Order</c> to display details for.</param>
        /// <param name="amountCharged">How much was charged for the <c>Order</c>.</param>
        public void DisplayDetail(StoreContext db, Order order, double? amountCharged)
        {
            var titleString = $"\nOrder placed on {order.TimeSubmitted} at {order.Location.Name} store.";
            Console.WriteLine(titleString);
            Console.Write(new string('=', titleString.Length));
            Console.Write("\n");

            var displayAlignment = "{0,-7}{1,-9}{2,-40}";
            var itemNameDividerSize = order.OrderLineItems.Max(li => li.Product.Name.Length);
            Console.Write(displayAlignment, "Qty", "Charged", "Item");
            Console.Write("\n");
            Console.Write(displayAlignment, "---", "-------", new string('-', itemNameDividerSize));
            Console.Write("\n");

            foreach (var li in order.OrderLineItems)
            {
                Console.WriteLine(displayAlignment,
                    li.Quantity, "$" + li.AmountCharged, li.Product.Name);
            }
            Console.Write(displayAlignment, "---", "-------", new string('-', itemNameDividerSize));
            Console.Write("\n");
            Console.WriteLine(displayAlignment, order.OrderLineItems.Sum(li => li.Quantity), "$" + amountCharged, "Total");
            CliInput.PressAnyKey();
        }

        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public OrderHistory(ApplicationData.State appState) : base(appState)
        {
            this.SortKey = OrderSortKey.DateDesc;
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Order History");
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var orders = db
                    .GetOrderHistory(this.ApplicationState.UserData.CustomerId)
                    .Select(o => new {
                        OrderId = o.OrderId,
                        Customer = o.Customer,
                        Location = o.Location,
                        TimeCreated = o.TimeCreated,
                        TimeSubmitted = o.TimeSubmitted,
                        TimeFulfilled = o.TimeFulfilled,
                        AmountPaid = o.AmountPaid,
                        OrderLineItem = o.OrderLineItems,
                        AmountCharged = (double)db.GetAmountCharged(o),
                    }).ToList();

                var upArrow = '↑';
                var downArrow = '↓';

                var priceSortSymbol = '-';
                var dateSortSymbol = '-';
                var locationSortSymbol = '-';

                switch (this.SortKey)
                {
                    case OrderSortKey.DateDesc:
                        orders = orders.OrderByDescending(o => o.TimeSubmitted).ToList();
                        dateSortSymbol = downArrow;
                        break;
                    case OrderSortKey.DateAsc:
                        orders = orders.OrderBy(o => o.TimeSubmitted).ToList();
                        dateSortSymbol = upArrow;
                        break;
                    case OrderSortKey.PriceDesc:
                        orders = orders.OrderByDescending(o => o.AmountCharged).ToList();
                        priceSortSymbol = downArrow;
                        break;
                    case OrderSortKey.PriceAsc:
                        orders = orders.OrderBy(o => o.AmountCharged).ToList();
                        priceSortSymbol = upArrow;
                        break;
                    case OrderSortKey.LocationDesc:
                        orders = orders.OrderByDescending(o => o.Location.Name).ToList();
                        locationSortSymbol = downArrow;
                        break;
                    case OrderSortKey.LocationAsc:
                        orders = orders.OrderBy(o => o.Location.Name).ToList();
                        locationSortSymbol = upArrow;
                        break;
                }

                var historyDisplayAlignment = "{0,-6}{1,-9}{2,-25}{3,-15}";
                var priceSortLine = $"{priceSortSymbol}----";
                var dateSortLine = $"{dateSortSymbol}---";
                var locationSortLine = $"{locationSortSymbol}----";

                Console.WriteLine(historyDisplayAlignment, "Num", "Price", "Date", "Store");
                Console.WriteLine(historyDisplayAlignment, "---", priceSortLine, dateSortLine, locationSortLine);
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

        /// <summary>
        /// Handle input.
        /// </summary>
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
                                    var amountCharged = db.GetAmountCharged(order);
                                    this.DisplayDetail(db, order, amountCharged);
                                    break;
                                }
                            }
                            else break;
                        }
                        catch (Exception) {
                            // We will just ignore parse errors and reprint the menu.
                            break;
                        }
                }
                this.PrintMenu();
            } while (true);
        }
    }
}

