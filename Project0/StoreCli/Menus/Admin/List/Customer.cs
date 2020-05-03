using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenuAdmin
{
    class Customer : CliMenu, IMenu
    {
        public Customer(ApplicationData.State appState): base(appState)
        {
            this.AddListMenuOption("Add Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddCustomer(appState));
            this.AddListMenuOption("Find Customer", ConsoleKey.D2, () => new StoreCliMenuAdmin.FindCustomer(appState));
        }
        public void PrintMenu()
        {
            this.PrintListMenu("Customer Management");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}