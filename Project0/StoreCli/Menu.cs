using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCli
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
        protected List<ListMenuOption> ListMenuOptions { get; } = new List<ListMenuOption>();

        public CliMenu(MenuController menuController)
        {
            this.MenuController = menuController;
        }

        protected void MenuAdd(IMenu menu)
        {
            this.MenuController.Push(menu);
        }

        protected void MenuExit()
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
                else if (cki.Key == ConsoleKey.Enter)
                {
                    return TryNavigateResult.Exit;

                }
            }
            return TryNavigateResult.NotFound;
        }

        protected void PrintListMenu()
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
        private DbContextOptions<StoreContext> contextOptions;
        public DbContextOptions<StoreContext> ContextOptions
        {
            get { return contextOptions; }
            private set { contextOptions = value; }
        }

        public void Push(IMenu menu)
        {
            this.Menus.Push(menu);
        }

        public void Pop()
        {
            this.Menus.Pop();
        }

        public MenuController(DbContextOptions<StoreContext> options)
        {
            this.ContextOptions = options;
        }

        public void Run()
        {
            Push(new StoreCliMenu.Main(this));

            while (this.Menus.Count > 0)
            {
                Console.WriteLine("call input loop");
                this.Menus.Peek().PrintMenu();
                this.Menus.Peek().InputLoop();
            }
        }
    }
}