using System;
using System.Linq;
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

            // This establishes a new database connection and will raise an exception if
            // there are any problems. We do this so we can detect any connection problems
            // immediately, and also so there is an active connection for later queries
            // (the user will not have to wait for a connection except on initial load).
            using (var db = new StoreDb.StoreContext(appState.DbOptions)){
                db.Locations.FirstOrDefault();
            }
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