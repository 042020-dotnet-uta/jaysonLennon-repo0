using System;
using StoreCli;
using StoreExtensions;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCliMenuUser
{
    class Login : CliMenu, IMenu
    {
        public Login(MenuController menuController): base(menuController) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Login");
        }

        public void InputLoop()
        {
            var dbOptions = this.MenuController.ContextOptions;
            var cliOptions = CliInput.GetLineOptions.TrimSpaces | CliInput.GetLineOptions.AbortOnEmpty;

            do
            {
                var login = CliInput.GetLine(cliOptions, CliInput.EmailValidator, "Login:");
                if (login == "" || login == null) {
                    this.MenuExit();
                    break;
                }

                var password = CliInput.GetPassword("Password:");

                if (dbOptions.VerifyCredentials(login, password))
                {
                    this.MenuExit();
                    this.MenuAdd(new StoreCliMenuUser.Main(this.MenuController));
                    break;
                } else {
                    CliPrinter.Error("Invalid login.");
                }
            } while (true);

        }
    }
}
