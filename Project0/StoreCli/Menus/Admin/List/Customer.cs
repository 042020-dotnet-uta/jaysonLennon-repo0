using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// List Menu for managing customers.
    /// </summary>
    class Customer : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Customer(ApplicationData.State appState): base(appState)
        {
            this.AddListMenuOption("Add Customer", ConsoleKey.D1, () => new StoreCliMenuAdmin.AddCustomer(appState));
            this.AddListMenuOption("Find Customer", ConsoleKey.D2, () => new StoreCliMenuAdmin.FindCustomer(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Customer Management");
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