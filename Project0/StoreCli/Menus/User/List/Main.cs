using System;
using Util;
using StoreExtensions;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Main menu after user login.
    /// </summary>
    class Main : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Main(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("Choose Store", ConsoleKey.D1, () => new StoreCliMenuUser.SelectLocation(appState));
            this.AddListMenuOption("Order Items", ConsoleKey.D2, () => new StoreCliMenuUser.OrderItems(appState));
            this.AddListMenuOption("Review Order", ConsoleKey.D3, () => new StoreCliMenuUser.ReviewOrder(appState));
            this.AddListMenuOption("Edit Order", ConsoleKey.D4, () => new StoreCliMenuUser.EditOrder(appState));
            this.AddListMenuOption("Order History", ConsoleKey.D5, () => new StoreCliMenuUser.OrderHistory(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("User Menu");
            var locationName = this.ApplicationState.UserData.OperatingLocationName;
            Console.Write($"\n\nStore: ");
            if (locationName != "" && locationName != null)
            {
                Console.Write($"{locationName}\n");
            }
            else
            {
                Console.Write("None selected\n");
            }
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
        public void InputLoop()
        {
            if (this.ApplicationState.UserData.OperatingLocationId == null)
            {
                this.MenuAdd(new StoreCliMenuUser.SelectLocation(this.ApplicationState));
                return;
            }
            this.RunListMenu(this.PrintMenu);
        }
    }
}
