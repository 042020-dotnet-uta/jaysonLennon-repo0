using System;
using Util;

namespace StoreCliMenuAdmin
{
    class LocationManagement : CliMenu, IMenu
    {
        public LocationManagement(ApplicationData.State appState): base(appState)
        {
            this.AddListMenuOption("Add New Location", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddLocation(appState));
            this.AddListMenuOption("Order History", ConsoleKey.D2, () => new StoreCliMenuAdmin.LocationOrderHistory(appState));
        }
        public void PrintMenu()
        {
            this.PrintListMenu("Location Management");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}