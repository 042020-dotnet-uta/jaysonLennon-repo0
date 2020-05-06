using System;
using StoreCliUtil;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Menu to create a new user account.
    /// </summary>
    class CreateAccount : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public CreateAccount(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Create Account");
        }

        /// <summary>
        /// Handle user input.
        /// </summary>
        public void InputLoop()
        {
            string EmailLoop(DbContextOptions<StoreContext> dbOptions)
            {
                var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;
                string login = "";
                do
                {
                    login = CliInput.GetLine(cliOptions, CliInput.EmailValidator, "Email Address:");

                    if (login == "" || login == null) return null;

                    if (dbOptions.LoginExists(login))
                        CliPrinter.Error("That email address is already in use.");
                    else break;
                } while (true);
                return login;
            }

            do
            {
                var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AcceptEmpty;

                var firstName = CliInput.GetLine(cliOptions, Customer.ValidateName, "First Name:");

                if (firstName == "" || firstName == null) {
                    this.AbortThenExit("Empty entry - exiting.");
                    return;
                }

                var lastName = CliInput.GetLine(cliOptions, Customer.ValidateName, "Last Name:");
                if (lastName == "" || lastName == null) {
                    this.AbortThenExit("Empty entry - exiting.");
                    return;
                }
                var login = EmailLoop(this.ApplicationState.DbOptions);
                if (login == "" || login == null) {
                    this.AbortThenExit("Empty entry - exiting.");
                    return;
                }

                var password = CliInput.GetPassword("Password:");
                if (password == "") {
                    this.AbortThenExit("Empty entry - exiting.");
                    return;
                }
                var customer = new Customer();
                // Data pre-validated above.
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.Login = login;
                customer.Password = password;

                var createResult = this.ApplicationState.DbOptions.CreateUserAccount(customer);

                if (createResult == CreateUserAccountResult.Ok) {
                    this.ApplicationState.UserData.CustomerId = customer.CustomerId;
                    this.MenuExit();
                    this.MenuAdd(new StoreCliMenuUser.Main(this.ApplicationState));
                    CliInput.PressAnyKey("\nAccount created.");
                    break;
                }
                else CliPrinter.Error("An error occurred while creating your account. Please try again.");
            } while (true);
        }
    }
}
