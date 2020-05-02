using System;
using StoreCli;
using StoreExtensions;

namespace StoreCliMenuUser
{
    class Login : CliMenu, IMenu
    {
        public Login(MenuController menuController): base(menuController) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("User Login");
        }

        public void InputLoop()
        {
            Console.Write("Username: ");
            var firstName = Console.ReadLine();
            Console.Write("Password: ");
            var password = CliInput.GetPassword();

            Console.WriteLine("\n Logging in...");

            Console.WriteLine("\nLogin complete. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}