using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;

namespace StoreCliMenuAdmin
{
    class Customer : CliMenu, IMenu
    {
        public Customer(MenuController menuController): base(menuController)
        {
            this.AddListMenuOption("Add Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddCustomer(this.MenuController));
            this.AddListMenuOption("Find Customer", ConsoleKey.D2, () => new StoreCliMenuAdmin.FindCustomer(this.MenuController));
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