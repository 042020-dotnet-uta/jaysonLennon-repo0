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
    class EditOrder : CliMenu, IMenu
    {
        private List<Guid> LineItemIds = new List<Guid>();

        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public EditOrder(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Edit Order");
            this.ApplicationState.UserData.RefreshCurrentOrder();

            this.LineItemIds.Clear();

            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var order = db.GetOrderById(this.ApplicationState.UserData.CurrentOrderId);
                if (order == null) return;

                var itemNameDividerSize = order.OrderLineItems.Max(li => li.Product.Name.Length);
                Console.WriteLine("#\tQty\tEach\tTotal\tName");
                Console.WriteLine($"===\t===\t=====\t=====\t{new string('=', itemNameDividerSize)}");

                double orderTotalCost = 0.0;
                foreach (var o in order.OrderLineItems)
                {
                    this.LineItemIds.Add(o.OrderLineItemId);
                    var lineItemTotal = o.Product.Price * o.Quantity;
                    orderTotalCost += lineItemTotal;
                    Console.WriteLine($"{this.LineItemIds.Count}\t{o.Quantity}\t${o.Product.Price}\t${lineItemTotal}\t{o.Product.Name}");
                }

                Console.WriteLine($"---\t-----\t-----\t-----\t{new string('-', itemNameDividerSize)}");
                Console.Write($"\t\t\t${orderTotalCost}\tTotal\n\n");
            }
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        public void InputLoop()
        {
            do
            {
                this.PrintMenu();
                if (this.ApplicationState.UserData.CurrentOrderId == null)
                {
                    CliInput.PressAnyKey("There is currently no open order.");
                    break;
                }

                var getLineOptions = CliInput.GetLineOptions.AcceptEmpty | CliInput.GetLineOptions.TrimSpaces;
                var option = CliInput.GetLine(getLineOptions, v => true, "[D]elete / Change [Q]uantity: ");
                switch (option)
                {
                    case null:
                    case "":
                        this.MenuExit();
                        return;
                    case "D":
                    case "d":
                    {
                        var itemNumber = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, n => n > 0 && n <= this.LineItemIds.Count, "Select item number:");
                        if (itemNumber == null) continue;

                        using (var db = new StoreContext(this.ApplicationState.DbOptions))
                        {
                            var deleted = db.DeleteLineItem(this.LineItemIds[(int)itemNumber - 1]);
                            if (!deleted) {
                                CliInput.PressAnyKey("There was a problem deleting that line item. Please try again.");
                            }
                        }
                        break;
                    }
                    case "Q":
                    case "q":
                    {
                        var itemNumber = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, n => n > 0 && n <= this.LineItemIds.Count, "Select item number:");
                        if (itemNumber == null) continue;

                        var newQuantity = CliInput.GetInt(CliInput.GetIntOptions.AllowEmpty, q => q >= 0, "New quantity:");
                        if (newQuantity == null) continue;

                        using (var db = new StoreContext(this.ApplicationState.DbOptions))
                        {
                            var adjusted = db.SetLineItemQuantity(this.LineItemIds[(int)itemNumber - 1], (int)newQuantity);
                            if (!adjusted) {
                                CliInput.PressAnyKey("There was a problem adjusting the quantity for that line item. Please try again.");
                            }
                        }
                        break;
                    }
                }

            } while (true);
            this.MenuExit();
        }
    }
}
