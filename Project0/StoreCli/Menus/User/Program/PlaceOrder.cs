using System;
using StoreCli;

namespace StoreCliMenuUser
{
    class PlaceOrder : CliMenu, IMenu
    {
        public PlaceOrder(MenuController menuController) : base(menuController)
        {
        }

        public void PrintMenu()
        {
            CliPrinter.Title("Place Order");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}
