using System;
using Util;

namespace StoreCliMenuAdmin
{
    class Main : CliMenu, IMenu
    {
        public Main(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.Customer(appState));
            this.AddListMenuOption("Query", ConsoleKey.D2, () => new StoreCliMenuAdmin.Query(appState));
            this.AddListMenuOption("Location Management", ConsoleKey.D3, () => new StoreCliMenuAdmin.LocationManagement(appState));
            this.AddListMenuOption("Master Menu List", ConsoleKey.D9, () => new StoreCliMenuAdmin.Master(appState));
        }

        public void PrintMenu()
        {
            this.PrintListMenu("Root Menu");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}