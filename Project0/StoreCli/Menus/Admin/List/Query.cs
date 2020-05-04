using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// List Menu for querying various data.
    /// </summary>
    class Query : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Query(ApplicationData.State appState): base(appState) {
            this.AddListMenuOption("Find Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.FindCustomer(appState));
            this.AddListMenuOption("Get orders by location", ConsoleKey.D2, () => new StoreCliMenuAdmin.LocationOrderHistory(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Query");
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}