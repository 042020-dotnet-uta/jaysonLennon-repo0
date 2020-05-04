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
            do
            {
                Console.Write("Location name: ");
                var name = Console.ReadLine();
                if (name.Trim() == "") {
                    this.AbortThenExit("No name provided.");
                    return;
                }

                var location = new StoreDb.Location(name);
                var added = this.ApplicationState.DbOptions.AddLocation(location);
                if (!added)
                {
                    CliPrinter.Error("A location with that name already exists.");
                    continue;
                }

                this.MenuExit();
                CliInput.PressAnyKey("Location added.");
                break;
            } while (true);
        }
    }
}