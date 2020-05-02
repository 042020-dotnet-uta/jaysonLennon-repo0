using System;
using StoreCli;

namespace StoreCliMenuAdmin
{
    class LocationManagement : CliMenu, IMenu
    {
        public LocationManagement(MenuController menuController): base(menuController)
        {
            this.AddListMenuOption("Add New Location", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddLocation(this.MenuController));
        }
        public void PrintMenu()
        {
            CliPrinter.Title("Location Management");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}