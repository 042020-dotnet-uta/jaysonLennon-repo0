using System;
using Util;

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
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Root Menu");
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