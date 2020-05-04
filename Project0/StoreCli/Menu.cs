using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace Util
{
    public enum TryNavigateResult
    {
        NavigationOk,
        NotFound,
        Exit,
    }
    public class ListMenuOption
    {
        public string Name;
        public Func<IMenu> CreateListMenu;
        public ConsoleKey Key;
    }

    public abstract class CliMenu
    {
        protected MenuController MenuController { get; private set; }
        private ApplicationData.State _ApplicationState;
        public ApplicationData.State ApplicationState
        {
            get { return _ApplicationState; }
            set { _ApplicationState = value; }
        }

        protected List<ListMenuOption> ListMenuOptions { get; set; } = new List<ListMenuOption>();

        public CliMenu(ApplicationData.State appState)
        {
            this.ApplicationState = appState;
            this.MenuController = appState.MenuController;
        }

        protected void MenuAdd(IMenu menu)
        {
            this.MenuController.Push(menu);
        }

        public void MenuExit()
        {
            this.MenuController.Pop();
        }

        protected void AddListMenuOption(String name, ConsoleKey key, Func<IMenu> createMenu)
        {
            this.ListMenuOptions.Add(new ListMenuOption { Name = name, Key = key, CreateListMenu = createMenu });
        }

        protected TryNavigateResult TryNavigate(ConsoleKeyInfo cki)
        {
            foreach (var option in this.ListMenuOptions)
            {
                if (cki.Key == option.Key)
                {
                    var newListMenu = option.CreateListMenu();
                    MenuAdd(newListMenu);
                    return TryNavigateResult.NavigationOk;
                }
                else if (cki.Key == ConsoleKey.Enter || cki.Key == ConsoleKey.Escape)
                {
                    return TryNavigateResult.Exit;

                }
            }
            return TryNavigateResult.NotFound;
        }

        protected void PrintListMenu(string title)
        {
            Console.Clear();
            foreach(var entry in this.ListMenuOptions)
            {
                Console.WriteLine($"{(char)entry.Key}.  {entry.Name}");
            }
        }

        protected void RunListMenu(Action printMenu)
        {
            ConsoleKeyInfo cki;

            do
            {
                cki = Console.ReadKey(true);

                var navResult = this.TryNavigate(cki);
                if (navResult == TryNavigateResult.NavigationOk) { break; }
                else if (navResult == TryNavigateResult.Exit) { this.MenuExit(); break; }

                printMenu();
            } while (true);

        }
    }

    public class MenuController
    {
        public Stack<IMenu> Menus { get; } = new Stack<IMenu>();
        public ApplicationData.State AppState { get; set; }
        
        public void Push(IMenu menu)
        {
            this.Menus.Push(menu);
        }

        public void Pop()
        {
            this.Menus.Pop();
        }

        private MenuController() {}
        public MenuController(ApplicationData.State appState)
        {
            this.AppState = appState;
        }

        public void Run()
        {
            Push(new StoreCliMenu.Main(this.AppState));

            while (this.Menus.Count > 0)
            {
                this.Menus.Peek().PrintMenu();
                this.Menus.Peek().InputLoop();
            }
        }
    }
}