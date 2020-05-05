using System;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;
using System.Linq;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// Menu to get the order history for a location.
    /// </summary>
    class LocationOrderHistory : CliMenu, IMenu
    {
        /// <summary>
        /// Key to use for sorting orders.
        /// </summary>
        private enum OrderSortKey
        {
            /// <summary>Sort by Date Ascending</summary>
            DateAsc,
            /// <summary>Sort by Date Descending</summary>
            DateDesc,
            /// <summary>Sort by Price Ascending</summary>
            PriceAsc,
            /// <summary>Sort by Price Descending</summary>
            PriceDesc,
        }

        /// <summary>
        /// Current sort key.
        /// </summary>
        private OrderSortKey SortKey { set; get; } = OrderSortKey.DateDesc;

        /// <summary>
        /// Possible modes this menu may operate in.
        /// </summary>
        private enum OperatingMode
        {
            /// <summary>Display and select locations.</summary>
            SelectLocation,
            /// <summary>View and sort orders.</summary>
            ViewOrders
        }

        /// <summary>
        /// The current operating mode.
        /// </summary>
        private OperatingMode CurrentOperatingMode { get; set; } = OperatingMode.SelectLocation;

        /// <summary>
        /// The location to display orders from.
        /// </summary>
        private Nullable<Guid> SelectedLocation { get; set; } = null;

        /// <summary>
        /// The order ids being displayed in the UI.
        /// </summary>
        private List<Guid> OrderIds { get; set; } = new List<Guid>();

        /// <summary>
        /// The location ids that are available to choose from.
        /// </summary>
        private List<Guid> LocationIds { get; set; } = new List<Guid>();

        /// <summary>
        /// Return value from input methods indicating the action to perform.
        /// </summary>
        private enum HandlerMsg
        {
            /// <summary>Menu should exit.</summary>
            Exit,
            /// <summary>Menu should continue to take input.</summary>
            Continue
        }

        /// <summary>
        /// Displays detailed information about an order.
        /// </summary>
        /// <param name="db">Store context.</param>
        /// <param name="order">Order to get information from.</param>
        /// <param name="amountCharged">Amount charged for the order.</param>
        public void DisplayDetail(StoreContext db, Order order, double? amountCharged)
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
            Console.WriteLine("{0,-7}{1,-9}", "Total", "$" + amountCharged);
            Console.WriteLine("{0,-7}{1,-9}", "Paid", "$" + order.AmountPaid);
            CliInput.PressAnyKey();
        }

        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public LocationOrderHistory(ApplicationData.State appState) : base(appState)
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
            switch (this.CurrentOperatingMode)
            {
                case OperatingMode.SelectLocation:
                    using (var db = new StoreContext(this.ApplicationState.DbOptions))
                    {
                        var locations = db.GetLocations();
                        this.LocationIds.Clear();
                        var i = 1;
                        foreach (var location in locations)
                        {
                            this.LocationIds.Add(location.LocationId);
                            Console.WriteLine($"{i}.  {location.Name}");
                            i += 1;
                        }
                        Console.WriteLine("\n");
                    }
                    break;
                case OperatingMode.ViewOrders:
                    using (var db = new StoreContext(this.ApplicationState.DbOptions))
                    {
                        var location = db.GetLocationById(this.SelectedLocation);
                        if (location == null) break;

                        var orders = db
                            .GetOrderHistory(location)
                            .Select(o => new {
                                OrderId = o.OrderId,
                                Customer = o.Customer,
                                Location = o.Location,
                                TimeCreated = o.TimeCreated,
                                TimeSubmitted = o.TimeSubmitted,
                                TimeFulfilled = o.TimeFulfilled,
                                AmountPaid = o.AmountPaid,
                                OrderLineItem = o.OrderLineItems,
                                AmountCharged = db.GetAmountCharged(o),
                            }).ToList();

                        if (orders.Count() == 0) {
                            CliPrinter.Error("There are no orders for this location.");
                            this.CurrentOperatingMode = OperatingMode.SelectLocation;
                            CliInput.PressAnyKey();
                            this.PrintMenu();
                            return;
                        }

                        var upArrow = '↑';
                        var downArrow = '↓';

                        var priceSortSymbol = '-';
                        var dateSortSymbol = '-';

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
                        }
                        var historyDisplayAlignment = "{0,-6}{1,-9}{2,-25}";
                        var priceSortLine = $"{priceSortSymbol}----";
                        var dateSortLine = $"{dateSortSymbol}---";

                        Console.WriteLine(historyDisplayAlignment, "Num", "Price", "Date");
                        Console.WriteLine(historyDisplayAlignment, "---", priceSortLine, dateSortLine);
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
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handle the input when viewing a list of all orders for a location.
        /// </summary>
        /// <returns>A <c>HandlerMsg</c> indicating what action should be taken.</returns>
        private HandlerMsg HandleViewOrderInput()
        {
            var inputOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;
            do
            {
                var line = CliInput.GetLine(inputOptions, v => true, "\nSort by [D]ate / [P]rice\nor enter an order number to view details:");
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
                    default:
                        if (line == "" || line == null)
                        {
                            this.CurrentOperatingMode = OperatingMode.SelectLocation;
                            return HandlerMsg.Continue;
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
                            else
                            {
                                CliPrinter.Error("Invalid order number");
                                continue;
                            }
                        }
                        catch
                        {
                            CliPrinter.Error("Invalid order number");
                            continue;
                        }
                }
                this.PrintMenu();
            } while (true);
        }

        /// <summary>
        /// Handle the input when selecting a location to query for orders.
        /// </summary>
        /// <returns>A <c>HandlerMsg</c> indicating what action should be taken.</returns>
        private HandlerMsg HandleSelectLocationInput()
        {
            var inputOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;
            do
            {
                var line = CliInput.GetLine(inputOptions, v => true, "Select location number:");
                if (line == "" || line == null) return HandlerMsg.Exit;
                try
                {
                    var locationNumber = Int32.Parse(line);
                    if (locationNumber > 0 && locationNumber <= this.LocationIds.Count)
                    {
                        this.SelectedLocation = this.LocationIds[locationNumber - 1];
                        this.CurrentOperatingMode = OperatingMode.ViewOrders;
                        return HandlerMsg.Continue;
                    }
                }
                catch
                {
                    CliPrinter.Error("Please enter a valid location number.");
                }
            } while (true);
        }

        /// <summary>
        /// Handle user input.
        /// </summary>
        public void InputLoop()
        {
            do
            {
                var handlerMsg = HandlerMsg.Continue;

                switch (this.CurrentOperatingMode)
                {
                    case OperatingMode.SelectLocation:
                        handlerMsg = HandleSelectLocationInput();
                        break;
                    case OperatingMode.ViewOrders:
                        handlerMsg = HandleViewOrderInput();
                        break;
                }
                if (handlerMsg == HandlerMsg.Exit)
                {
                    this.MenuExit();
                    return;
                }
                this.PrintMenu();
            } while (true);
        }
    }
}

