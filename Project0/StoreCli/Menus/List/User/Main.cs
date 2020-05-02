using System;
using StoreCli;

namespace StoreCliMenuUser
{
    class Main : CliMenu, IMenu
    {
        public Main(MenuController menuController) : base(menuController)
        {
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
