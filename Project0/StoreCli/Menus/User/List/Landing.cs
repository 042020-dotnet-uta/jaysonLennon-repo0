using System;
using StoreCliUtil;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Root user menu.
    /// </summary>
    class Landing : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Landing(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("Login", ConsoleKey.D1, () => new StoreCliMenuUser.Login(appState));
            this.AddListMenuOption("Create Account", ConsoleKey.D2, () => new StoreCliMenuUser.CreateAccount(appState));
        }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Landing");
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}
