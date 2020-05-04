using System;
using Util;

namespace StoreCliMenuAdmin
{
    /// <summary>
    /// Master list of menus.
    /// </summary>
    class Master : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Master(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Print this menu.
        /// </summary>
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

        /// <summary>
        /// Handle user input.
        /// </summary>
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