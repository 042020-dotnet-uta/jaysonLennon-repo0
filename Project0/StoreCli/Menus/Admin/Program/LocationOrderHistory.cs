using System;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;
using System.Linq;

namespace StoreCliMenuAdmin
{
    class LocationOrderHistory : CliMenu, IMenu
    {
        private enum OrderSortKey
        {
            DateAsc,
            DateDesc,
            PriceAsc,
            PriceDesc,
        }
        private OrderSortKey SortKey { set; get; }

        private enum OperatingMode
        {
            SelectLocation,
            ViewOrders
        }
        private OperatingMode CurrentOperatingMode { get; set; } = OperatingMode.SelectLocation;
        private Nullable<Guid> SelectedLocation { get; set; } = null;
        private List<Guid> OrderIds { get; } = new List<Guid>();
        private List<Guid> LocationIds { get; } = new List<Guid>();

        private enum HandlerMsg
        {
            Exit,
            Continue
        }

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

        public LocationOrderHistory(ApplicationData.State appState) : base(appState)
        {
            this.SortKey = OrderSortKey.DateDesc;
        }

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

                        var orders = db.GetOrderHistory(location);
                        if (orders.Count() == 0) {
                            CliPrinter.Error("There are no orders for this location.");
                            this.CurrentOperatingMode = OperatingMode.SelectLocation;
                            CliInput.PressAnyKey();
                            this.PrintMenu();
                            return;
                        }
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
                        }
                        var historyDisplayAlignment = "{0,-5}{1,-7}{2,-25}";
                        Console.WriteLine(historyDisplayAlignment, "Num", "Price", "Date");
                        Console.WriteLine(historyDisplayAlignment, "---", "-----", "----");
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
                                    this.DisplayDetail(db, order);
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

