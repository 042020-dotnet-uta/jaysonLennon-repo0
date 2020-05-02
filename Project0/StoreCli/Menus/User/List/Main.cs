using System;
using StoreCli;

namespace StoreCliMenuUser
{
    class Main : CliMenu, IMenu
    {
        public Main(MenuController menuController) : base(menuController)
        {
            this.AddListMenuOption("Login", ConsoleKey.D1, () => new StoreCliMenuUser.Login(this.MenuController));
            this.AddListMenuOption("Create Account", ConsoleKey.D2, () => new StoreCliMenuUser.CreateAccount(this.MenuController));
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
