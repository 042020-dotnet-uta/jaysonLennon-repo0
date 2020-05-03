using System;
using Util;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCliMenuUser
{
    class CreateAccount : CliMenu, IMenu
    {
        public CreateAccount(ApplicationData.State appState): base(appState) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Create Account");
        }

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

                var firstName = CliInput.GetLine(cliOptions, CliInput.NameValidator, "First Name:");
                if (firstName == "" || firstName == null) {
                    this.AbortThenExit("Empty entry - exiting.");
                    return;
                }
                var lastName = CliInput.GetLine(cliOptions, CliInput.NameValidator, "Last Name:");
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
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.Login = login;
                customer.Password = password;

                var createResult = this.ApplicationState.DbOptions.CreateUserAccount(customer);
                if (createResult == CreateUserAccountResult.Ok) {
                    this.MenuExit();
                    this.MenuAdd(new StoreCliMenuUser.Main(this.ApplicationState));
                    CliInput.PressAnyKey("\nAccount created. Press any key to continue.");
                    break;
                }
                else CliPrinter.Error("An error occurred while creating your account. Please try again.");
            } while (true);
        }
    }
}
