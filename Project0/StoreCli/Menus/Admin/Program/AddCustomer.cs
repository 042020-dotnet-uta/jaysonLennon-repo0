using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;
using StoreExtensions;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// Menu to add new customer
    /// </summary>
    // TODO: delete or change admin customer creation menu.
    class AddCustomer : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public AddCustomer(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Prints this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Create new customer");
        }

        /// <summary>
        /// Handle user input.
        /// </summary>
        public void InputLoop()
        {
            // TODO: validate customer first name.
            Console.Write("Customer first name: ");
            var firstName = Console.ReadLine();
            // TODO: validate customer last name.
            Console.Write("Customer last name: ");
            var lastName = Console.ReadLine();

            var customer = new StoreDb.Customer(firstName);
            customer.LastName = lastName;

            // TODO: get customer email.

            Console.WriteLine("\nAdding new customer...");

            this.ApplicationState.DbOptions.AddCustomer(customer);

            Console.WriteLine("\nCustomer added. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}