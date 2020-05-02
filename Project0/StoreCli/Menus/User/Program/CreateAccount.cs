using System;
using StoreCli;

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
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            Console.Write("Email Address: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = CliInput.GetPassword();

            Console.WriteLine("\n Logging in...");

            Console.WriteLine("\nLogin complete. Press any key to return.");
            Console.ReadKey(true);

            this.MenuExit();
        }
    }
}
