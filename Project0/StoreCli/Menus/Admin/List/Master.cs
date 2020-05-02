using System;
using StoreCli;

namespace StoreCliMenuAdmin
{
    class Master : CliMenu, IMenu
    {
        public Master(MenuController menuController): base(menuController)
        { }
        public void PrintMenu()
        {
            Console.Clear();

            CliPrinter.Title("Master Menu List");

            Console.WriteLine("10. User: Create Account");
            Console.WriteLine("11. User: Login");
            Console.WriteLine("12. User: Place Order");
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
                            this.MenuAdd(new StoreCliMenuUser.CreateAccount(this.MenuController));
                            return;
                        case 11:
                            this.MenuAdd(new StoreCliMenuUser.Login(this.MenuController));
                            return;
                        case 12:
                            this.MenuAdd(new StoreCliMenuUser.PlaceOrder(this.MenuController));
                            return;
                        case 30:
                            this.MenuAdd(new StoreCliMenuAdmin.AddCustomer(this.MenuController));
                            return;
                        case 31:
                            this.MenuAdd(new StoreCliMenuAdmin.AddLocation(this.MenuController));
                            return;
                        case 32:
                            this.MenuAdd(new StoreCliMenuAdmin.FindCustomer(this.MenuController));
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