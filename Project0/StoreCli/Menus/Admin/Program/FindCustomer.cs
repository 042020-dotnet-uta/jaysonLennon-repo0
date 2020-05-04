using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;
using StoreExtensions;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// Menu to query customers by name.
    /// </summary>
    class FindCustomer : CliMenu, IMenu
    {
        /// <summary>
        /// The terminal row to print the results to.
        /// </summary>
        private const int ResultCliRow = 4;

        /// <summary>
        /// The terminal row to print the query to.
        /// </summary>
        private const int QueryCliRow = 2;

        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public FindCustomer(ApplicationData.State appState) : base(appState) { }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Find Customer");
        }

        /// <summary>
        /// Clear search results from the terminal.
        /// </summary>
        private void ClearResults()
        {
            //Console.SetCursorPosition(0, ResultRow + 1);
            for (var i = ResultCliRow + 1; i < Console.WindowHeight; i++)
            {
                ClearConsoleRow(i);
                //Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        /// <summary>
        /// Clear a single for from the terminal.
        /// </summary>
        /// <param name="row">The row number to clear.</param>
        private void ClearConsoleRow(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        /// <summary>
        /// Print the user search term to the terminal.
        /// </summary>
        /// <param name="term">The term print.</param>
        private void DisplaySearchTerm(string term)
        {
            ClearConsoleRow(QueryCliRow);
            Console.SetCursorPosition(0, QueryCliRow);
            Console.Write("Search: ");
            Console.Write(term);
            Console.Write("_");
        }

        /// <summary>
        /// Handle user input.
        /// </summary>
        public void InputLoop()
        {

            ConsoleKeyInfo cki;
            string searchTerm = "";

            this.DisplaySearchTerm("");
            Console.SetCursorPosition(0, ResultCliRow);
            Console.WriteLine("=============================");

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

                Console.SetCursorPosition(0, ResultCliRow);
                Console.WriteLine("=============================");

                if (searchTerm.Length == 0) continue;

                using (var db = new StoreDb.StoreContext(this.ApplicationState.DbOptions))
                {
                    var customers = db.FindCustomerByName(searchTerm).Take(20);
                    Console.Write($"{customers.Count()} customers found.\n\n");
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