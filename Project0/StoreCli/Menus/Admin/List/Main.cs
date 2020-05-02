using System;
using StoreCli;

namespace StoreCliMenuAdmin
{
    class Main : CliMenu, IMenu
    {
        public Main(MenuController menuController) : base(menuController)
        {
            this.AddListMenuOption("Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.Customer(this.MenuController));
            this.AddListMenuOption("Query", ConsoleKey.D2, () => new StoreCliMenuAdmin.Query(this.MenuController));
            this.AddListMenuOption("Location Management", ConsoleKey.D3, () => new StoreCliMenuAdmin.LocationManagement(this.MenuController));
            this.AddListMenuOption("Master Menu List", ConsoleKey.D9, () => new StoreCliMenuAdmin.Master(this.MenuController));
        }

        public void PrintMenu()
        {
            CliPrinter.Title("Main");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}