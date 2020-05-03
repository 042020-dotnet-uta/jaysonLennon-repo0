using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Util;

namespace StoreCliMenu
{
    class Main : CliMenu, IMenu
    {
        public Main(ApplicationData.State appState) : base(appState)
        {
            this.AddListMenuOption("User", ConsoleKey.D1, () => new StoreCliMenuUser.Landing(this.ApplicationState));
            this.AddListMenuOption("Administrator", ConsoleKey.D2, () => new StoreCliMenuAdmin.Main(this.ApplicationState));
        }

        public void PrintMenu()
        {
            this.PrintListMenu("Main");
        }

        public void InputLoop()
        {
            this.RunListMenu(this.PrintMenu);
        }
    }
}