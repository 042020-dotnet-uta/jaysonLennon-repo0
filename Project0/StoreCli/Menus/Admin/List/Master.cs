using System;
using Util;

namespace StoreCliMenuAdmin
{
    class Master : CliMenu, IMenu
    {
        public Master(ApplicationData.State appState): base(appState)
        { }
        public void PrintMenu()
        {
            Console.Clear();

            CliPrinter.Title("Master Program List");

            Console.WriteLine("10. User: Create Account");
            Console.WriteLine("11. User: Login");
            Console.WriteLine("12. User: Select Location");
            Console.WriteLine("30. Admin: Add Customer");
            Console.WriteLine("31. Admin: Add Location");
            Console.WriteLine("32. Admin: Find Customer");
        }

        public void InputLoop()
        {
            do
            {
                Console.Write("\nMenu number: ");
                var input = Console.ReadLine();
                if (input == null || input.Trim() == "")
                {
                    this.MenuExit();
                    return;
                }

                try
                {
                    var menuNumber = Int32.Parse(input);
                    switch (menuNumber)
                    {
                        case 10:
                            this.MenuAdd(new StoreCliMenuUser.CreateAccount(this.ApplicationState));
                            return;
                        case 11:
                            this.MenuAdd(new StoreCliMenuUser.Login(this.ApplicationState));
                            return;
                        case 12:
                            this.MenuAdd(new StoreCliMenuUser.SelectLocation(this.ApplicationState));
                            return;
                        case 30:
                            this.MenuAdd(new StoreCliMenuAdmin.AddCustomer(this.ApplicationState));
                            return;
                        case 31:
                            this.MenuAdd(new StoreCliMenuAdmin.AddLocation(this.ApplicationState));
                            return;
                        case 32:
                            this.MenuAdd(new StoreCliMenuAdmin.FindCustomer(this.ApplicationState));
                            return;
                        default:
                            CliPrinter.Error("Invalid entry");
                            break;
                    }
                    this.PrintMenu();
                } catch { continue; }
            } while (true);
        }
    }
}