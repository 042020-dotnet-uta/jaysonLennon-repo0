using System;
using StoreCliUtil;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// The Main menu.
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
            this.AddListMenuOption("Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.Customer(appState));
            this.AddListMenuOption("Query", ConsoleKey.D2, () => new StoreCliMenuAdmin.Query(appState));
            this.AddListMenuOption("Location Management", ConsoleKey.D3, () => new StoreCliMenuAdmin.LocationManagement(appState));
            this.AddListMenuSpace();

            this.AddListMenuOption("Find customer by name", ConsoleKey.D4, () => new StoreCliMenuAdmin.FindCustomer(appState));
            this.AddListMenuOption("Add new customer", ConsoleKey.D5, () => new StoreCliMenuAdmin.AddCustomer(appState));
            this.AddListMenuOption("Add new location", ConsoleKey.D6, () => new StoreCliMenuAdmin.AddLocation(appState));
            this.AddListMenuOption("Location order history", ConsoleKey.D7, () => new StoreCliMenuAdmin.LocationOrderHistory(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Administration Menu");
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}