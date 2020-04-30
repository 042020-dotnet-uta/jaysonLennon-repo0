using System;
using StoreCli;

namespace StoreCliMenu
{
    class LocationAdmin : CliMenu, IMenu
    {
        public LocationAdmin(MenuController menuController): base(menuController)
        {
            this.AddListMenuOption("Add Location", ConsoleKey.D1, () => new StoreCliMenu.AddLocation(this.MenuController));
        }
        public void PrintMenu()
        {
            CliPrinter.Title("Location Administration");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}