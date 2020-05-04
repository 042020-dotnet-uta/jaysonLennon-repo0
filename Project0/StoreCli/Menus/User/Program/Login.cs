using System;
using Util;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCliMenuUser
{
    /// <summary>
    /// User login menu.
    /// </summary>
    class Login : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Login(ApplicationData.State appState): base(appState) { }
        
        /// <summary>
        /// Print this menu
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Login");
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        public void InputLoop()
        {
            var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;

            do
            {
                var login = CliInput.GetLine(cliOptions, CliInput.EmailValidator, "Login:");
                if (login == "" || login == null) {
                    this.MenuExit();
                    break;
                }

                var password = CliInput.GetPassword("Password:");

                var customerId = this.ApplicationState.DbOptions.VerifyCredentials(login, password);
                if (customerId != null)
                {
                    this.ApplicationState.CustomerId = customerId;
                    this.MenuExit();
                    this.MenuAdd(new StoreCliMenuUser.Main(this.ApplicationState));
                    break;
                } else {
                    CliPrinter.Error("Invalid login.");
                }
            } while (true);
        }
    }
}
