using System;
using StoreCli;

namespace StoreCliMenuUser
{
    class Main : CliMenu, IMenu
    {
        public Main(MenuController menuController) : base(menuController)
        {
            this.AddListMenuOption("Place Order", ConsoleKey.D1, () => new StoreCliMenuUser.PlaceOrder(this.MenuController));
        }

        public void PrintMenu()
        {
            CliPrinter.Title("User Menu");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}
