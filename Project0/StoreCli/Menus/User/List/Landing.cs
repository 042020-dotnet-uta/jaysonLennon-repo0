using System;
using Util;

namespace StoreCliMenuUser
{
    class Landing : CliMenu, IMenu
    {
        public Landing(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("Login", ConsoleKey.D1, () => new StoreCliMenuUser.Login(appState));
            this.AddListMenuOption("Create Account", ConsoleKey.D2, () => new StoreCliMenuUser.CreateAccount(appState));
        }

        public void PrintMenu()
        {
            this.PrintListMenu("Landing");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}
