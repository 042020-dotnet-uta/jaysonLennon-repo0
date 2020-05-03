using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;
using StoreExtensions;

namespace StoreCliMenuAdmin
{
    class AddCustomer : CliMenu, IMenu
    {
        public AddCustomer(ApplicationData.State appState): base(appState) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Create new customer");
        }

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