using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreCli;
using StoreExtensions;

namespace StoreCliMenu
{
    class FindCustomer : CliMenu, IMenu
    {
        private const int ResultRow = 4;
        private const int QueryRow = 2;

        public FindCustomer(MenuController menuController) : base(menuController) { }
        public void PrintMenu()
        {
            Console.Clear();
            Console.Write("--- Find Customer ---\n\n");
        }

        private void ClearResults()
        {
            //Console.SetCursorPosition(0, ResultRow + 1);
            for (var i = ResultRow + 1; i < Console.WindowHeight; i++)
            {
                ClearConsoleRow(i);
                //Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        private void ClearConsoleRow(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        private void DisplaySearchTerm(string term)
        {
            ClearConsoleRow(QueryRow);
            Console.SetCursorPosition(0, QueryRow);
            Console.Write(term);
        }

        public void InputLoop()
        {

            ConsoleKeyInfo cki;
            string searchTerm = "";

            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    // Exit searching if search term is empty,
                    // otherwise we will just clear the search term.
                    if (searchTerm.Trim() == "") break;
                    else searchTerm = "";
                }
                else if (cki.Key == ConsoleKey.Backspace) {
                    if (searchTerm.Length > 0) {
                        searchTerm = searchTerm.Substring(0, searchTerm.Length - 1);
                    } else {
                        searchTerm = "";
                    }
                }
                // Always exit when using Esc key.
                else if (cki.Key == ConsoleKey.Escape) break;
                else searchTerm += cki.KeyChar;

                DisplaySearchTerm(searchTerm);

                ClearResults();

                Console.SetCursorPosition(0, ResultRow);
                Console.WriteLine("=============================");

                using (var db = new StoreDb.StoreContext(this.MenuController.ContextOptions))
                {
                    var customers = db.FindCustomerByName(searchTerm);
                    Console.WriteLine($"customer count = {customers.Count()}");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerId}: {customer.FirstName} {customer.LastName}");
                    }
                }

            } while (true);

            MenuExit();
        }
    }
}