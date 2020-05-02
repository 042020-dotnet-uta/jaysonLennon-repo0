using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;

namespace StoreCliMenu
{
    class Main : CliMenu, IMenu
    {
        public Main(MenuController menuController) : base(menuController)
        {
            this.AddListMenuOption("User", ConsoleKey.D1, () => new StoreCliMenuUser.Main(this.MenuController));
            this.AddListMenuOption("Administrator", ConsoleKey.D2, () => new StoreCliMenuAdmin.Main(this.MenuController));
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