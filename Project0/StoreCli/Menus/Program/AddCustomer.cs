using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;
using StoreExtensions;

namespace StoreCliMenu
{
    class AddCustomer : CliMenu, IMenu
    {
        public AddCustomer(MenuController menuController): base(menuController) { }
        public void PrintMenu()
        {
            Console.Clear();
            Console.Write("--- Create new customer ---\n\n");
        }

        public void InputLoop()
        {
            Console.Write("Customer first name: ");
            var name = Console.ReadLine();
            var customer = new StoreDb.Customer(name);

            Console.WriteLine("\nAdding new customer...");

            this.MenuController.ContextOptions.AddCustomer(customer);

            Console.WriteLine("\nCustomer added. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}