using System;
using StoreCli;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCliMenuUser
{
    class CreateAccount : CliMenu, IMenu
    {
        public CreateAccount(MenuController menuController): base(menuController) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Create Account");
        }

        public void InputLoop()
        {
            string PasswordLoop()
            {
                var pass = "";
                do {
                    Console.Write("Create Password: ");
                    pass = CliInput.GetPassword();
                    if (pass == "") CliPrinter.Error("Please provide a password.");
                } while (pass == "");
                return pass;
            }

            string EmailLoop(DbContextOptions<StoreContext> dbOptions)
            {
                var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.Required;
                string login = "";
                do
                {
                    login = CliInput.GetLine(cliOptions, CliInput.EmailValidator, "Email Address:");
                    if (dbOptions.LoginExists(login))
                        CliPrinter.Error("That email address is already in use.");
                    else break;
                } while (true);
                return login;
            }

            var dbOptions = this.MenuController.ContextOptions;
            var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.Required;

            var customer = new StoreDb.Customer();
            customer.FirstName = CliInput.GetLine(cliOptions, CliInput.NameValidator, "First Name:");
            customer.LastName = CliInput.GetLine(cliOptions, CliInput.NameValidator, "Last Name:");
            customer.Login = EmailLoop(dbOptions);
            customer.Password = PasswordLoop();

            // TODO: go straight to the user options.
            this.AbortThenExit("Account created.");
        }
    }
}
