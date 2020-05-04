using System;
using Util;
using StoreExtensions;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// Menu to add a new location.
    /// </summary>
    class AddLocation : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public AddLocation(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Prints this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Add New Location");
        }

        /// <summary>
        /// Handles user input.
        /// </summary>
        public void InputLoop()
        {
            Console.Write("Location name: ");
            var name = Console.ReadLine();
            if (name.Trim() == "") {
                this.AbortThenExit("No name provided.");
                return;
            }

            Console.WriteLine("\nAdding new location...");

            var location = new StoreDb.Location(name);
            this.ApplicationState.DbOptions.AddLocation(location);

            Console.WriteLine("\nLocation added. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}