using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;

namespace StoreCliMenu
{
    class Customer : CliMenu, IMenu
    {
        public Customer(MenuController menuController): base(menuController)
        {
            this.AddListMenuOption("Add Customer", ConsoleKey.D1, () => new StoreCliMenu.AddCustomer(this.MenuController));
            this.AddListMenuOption("Find Customer", ConsoleKey.D2, () => new StoreCliMenu.FindCustomer(this.MenuController));
        }
        public void PrintMenu()
        {
            CliPrinter.Title("Customer");
            this.PrintListMenu();
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}