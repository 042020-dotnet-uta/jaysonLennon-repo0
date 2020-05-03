using System;
using Util;
using StoreExtensions;

namespace StoreCliMenuUser
{
    class Main : CliMenu, IMenu
    {
        private Nullable<Guid> GetDefaultLocationId()
        {
            return this.ApplicationState.DbOptions.GetDefaultLocation(this.ApplicationState.CustomerId);
        }

        public Main(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("Set default store", ConsoleKey.D1, () => new StoreCliMenuUser.SelectLocation(appState));
            this.ApplicationState.OperatingLocationId = GetDefaultLocationId();
            Console.Write($"set appstate store to {this.ApplicationState.OperatingLocationId}");
            Console.ReadKey();
        }

        public void PrintMenu()
        {
            this.PrintListMenu("User Menu");

            using (var db = new StoreDb.StoreContext(this.ApplicationState.DbOptions))
            {
                var location = db.GetLocation(this.ApplicationState.OperatingLocationId);
                Console.Write($"\n\nDefault store: ");
                if (location != null)
                {
                    Console.Write($"{location.Name}\n");
                }
                else
                {
                    Console.Write("None selected\n");
                }
            }
        }
        public void InputLoop()
        {
            if (GetDefaultLocationId() == null)
            {
                this.MenuAdd(new StoreCliMenuUser.SelectLocation(this.ApplicationState));
                return;
            }
            this.RunListMenu(this.PrintMenu);
        }
    }
}
