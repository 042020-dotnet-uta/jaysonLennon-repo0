using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenuAdmin
{
    class Query : CliMenu, IMenu
    {
        public Query(ApplicationData.State appState): base(appState) {
            this.AddListMenuOption("Find Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.FindCustomer(appState));
        }
        public void PrintMenu()
        {
            this.PrintListMenu("Query");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}