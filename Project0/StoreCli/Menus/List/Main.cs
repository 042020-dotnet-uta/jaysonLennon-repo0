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
            this.AddListMenuOption("Customer", ConsoleKey.D1, () => new StoreCliMenu.Customer(this.MenuController));
            this.AddListMenuOption("Query", ConsoleKey.D2, () => new StoreCliMenu.Query(this.MenuController));
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