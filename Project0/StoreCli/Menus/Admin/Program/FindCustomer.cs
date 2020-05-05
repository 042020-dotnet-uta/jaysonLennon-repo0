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
        private const int SearchResultTerminalRow = 4;

        /// <summary>
        /// The terminal row to print the query to.
        /// </summary>
        private const int QueryEntryTerminalRow = 2;

        private IQueryable<Customer> SearchResults;
        private string SearchQuery = "";

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
            this.DisplaySearchTerm(this.SearchQuery);
            Console.SetCursorPosition(0, SearchResultTerminalRow);

            DisplaySearchTerm(this.SearchQuery);

            ClearResults();

            Console.SetCursorPosition(0, SearchResultTerminalRow);
            Console.WriteLine("=============================");

            if (this.SearchQuery.Length == 0) return;

            var maxResults = Console.WindowHeight - SearchResultTerminalRow - 4;
            using (var db = new StoreDb.StoreContext(this.ApplicationState.DbOptions))
            {
                var customers = db.FindCustomerByName(this.SearchQuery);
                var displayAmount = customers.Count() > maxResults ? maxResults : customers.Count();
                Console.Write($"{customers.Count()} customers found. Displaying {displayAmount}.\n\n");
                foreach (var customer in customers.Take(displayAmount).OrderBy(c => c.FirstName))
                {
                    var phoneNumber = customer.PhoneNumber;
                    if (phoneNumber == null || phoneNumber == "") phoneNumber = "            ";
                    Console.WriteLine($"{phoneNumber}: {customer.FirstName} {customer.LastName}");
                }
            }
        }

        /// <summary>
        /// Clear search results from the terminal.
        /// </summary>
        private void ClearResults()
        {
            //Console.SetCursorPosition(0, ResultRow + 1);
            for (var i = SearchResultTerminalRow + 1; i < Console.WindowHeight; i++)
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
            ClearConsoleRow(QueryEntryTerminalRow);
            Console.SetCursorPosition(0, QueryEntryTerminalRow);
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
            this.SearchQuery = "";

            Console.WriteLine("=============================");

            do
            {
                PrintMenu();
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    // Exit searching if search term is empty,
                    // otherwise we will just clear the search term.
                    if (this.SearchQuery.Trim() == "") break;
                    else this.SearchQuery = "";
                }
                else if (cki.Key == ConsoleKey.Backspace) {
                    if (this.SearchQuery.Length > 0) {
                        this.SearchQuery = this.SearchQuery.Substring(0, this.SearchQuery.Length - 1);
                    } else {
                        this.SearchQuery = "";
                    }
                }
                // Always exit when using Esc key.
                else if (cki.Key == ConsoleKey.Escape) break;
                // Ignore non-printable characters.
                // https://docs.microsoft.com/en-us/dotnet/api/system.consolekeyinfo.keychar?view=netcore-3.1#remarks
                else if (cki.KeyChar == '\u0000') continue;
                else this.SearchQuery += cki.KeyChar;

            } while (true);

            MenuExit();
        }
    }
}