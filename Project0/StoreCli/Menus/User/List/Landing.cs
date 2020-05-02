using System;
using StoreCli;

namespace StoreCliMenuUser
{
    class Landing : CliMenu, IMenu
    {
        public Landing(MenuController menuController) : base(menuController)
        {
            this.AddListMenuOption("Login", ConsoleKey.D1, () => new StoreCliMenuUser.Login(this.MenuController));
            this.AddListMenuOption("Create Account", ConsoleKey.D2, () => new StoreCliMenuUser.CreateAccount(this.MenuController));
        }

        public void PrintMenu()
        {
            CliPrinter.Title("Landing");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}
