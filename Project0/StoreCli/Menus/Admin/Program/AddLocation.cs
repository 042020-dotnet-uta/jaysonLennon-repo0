using System;
using Util;
using StoreExtensions;

namespace StoreCliMenuAdmin
{
    class AddLocation : CliMenu, IMenu
    {
        public AddLocation(ApplicationData.State appState): base(appState) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Add New Location");
        }

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