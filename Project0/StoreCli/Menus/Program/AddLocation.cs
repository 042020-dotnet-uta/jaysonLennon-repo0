using System;
using StoreCli;
using StoreExtensions;

namespace StoreCliMenu
{
    class AddLocation : CliMenu, IMenu
    {
        public AddLocation(MenuController menuController): base(menuController) { }
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
            this.MenuController.ContextOptions.AddLocation(location);

            Console.WriteLine("\nLocation added. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}