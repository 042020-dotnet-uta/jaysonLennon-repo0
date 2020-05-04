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
            this.AddListMenuOption("Set default store", ConsoleKey.D1, () => new StoreCliMenuUser.SelectLocation(appState));
            this.AddListMenuOption("Order Items", ConsoleKey.D2, () => new StoreCliMenuUser.OrderItems(appState));
            this.AddListMenuOption("Review Order", ConsoleKey.D3, () => new StoreCliMenuUser.ReviewOrder(appState));
            this.AddListMenuOption("Order History", ConsoleKey.D4, () => new StoreCliMenuUser.OrderHistory(appState));

            appState.UserData.RefreshDefaultLocation();
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("User Menu");

            using (var db = new StoreDb.StoreContext(this.ApplicationState.DbOptions))
            {
                var location = db.GetLocation(this.ApplicationState.UserData.OperatingLocationId);
                Console.Write($"\n\nDefault store: ");
                if (location != null)
                {
                    Console.Write($"{location.Name}\n");
                }
                else
                {
                    Console.Write("None selected\n");
                }
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
