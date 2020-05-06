using System;
using StoreCliUtil;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// List Menu for managing locations.
    /// </summary>
    class LocationManagement : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public LocationManagement(ApplicationData.State appState): base(appState)
        {
            this.AddListMenuOption("Add New Location", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddLocation(appState));
            this.AddListMenuOption("Order History", ConsoleKey.D2, () => new StoreCliMenuAdmin.LocationOrderHistory(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Location Management");
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