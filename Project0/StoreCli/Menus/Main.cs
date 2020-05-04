using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenu
{
    /// <summary>
    /// The Main menu.
    /// </summary>
    class Main : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public Main(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("User", ConsoleKey.D1, () => new StoreCliMenuUser.Landing(this.ApplicationState));
            this.AddListMenuOption("Administrator", ConsoleKey.D2, () => new StoreCliMenuAdmin.Main(this.ApplicationState));
        }

        /// <summary>
        /// Prints the menu.
        /// </summary>
        public void PrintMenu()
        {
            this.PrintListMenu("Main");
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}