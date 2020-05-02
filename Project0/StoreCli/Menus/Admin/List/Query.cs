using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;

namespace StoreCliMenuAdmin
{
    class Query : CliMenu, IMenu
    {
        public Query(MenuController menuController): base(menuController) {
            this.AddListMenuOption("Find Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.FindCustomer(this.MenuController));
        }
        public void PrintMenu()
        {
            CliPrinter.Title("Query");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}