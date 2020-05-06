using System;
using System.Collections.Generic;

namespace StoreCliUtil
{
    /// <summary>
    /// Result from calling <c>TryNavigate()</c>.
    /// This governs the action that should be taken by the caller after
    /// running <c>TryNavigate()</c>
    /// </summary>
    public enum TryNavigateResult
    {
        /// <summary>The navigation operation succeeded.</summary>
        NavigationOk,
        /// <summary>The navigation entry was not found.</summary>
        NotFound,
        /// <summary>Exit the menu.</summary>
        Exit,
    }

    /// <summary>
    /// A menu option in a List Menu.
    /// 
    /// This is used when creating a list of possible menu selections
    /// and the user is prompted to pick one of the items from the list.
    /// </summary>
    public class ListMenuOption
    {
        /// <summary>The text to display as the menu name.</summary>
        public string Name;
        /// <summary>Function to create the list menu.</summary>
        public Func<IMenu> CreateListMenu;
        /// <summary>The key to press in order to access the menu item.</summary>
        public ConsoleKey Key;
    }

    /// <summary>
    /// Base class for all List menus.
    /// </summary>
    public abstract class CliMenu
    {
        /// <summary>
        /// Manages the menu stack.
        /// </summary>
        protected MenuController MenuController { get; private set; }

        private ApplicationData.State _ApplicationState;

        /// <summary>
        /// Application state data.
        /// </summary>
        public ApplicationData.State ApplicationState
        {
            get { return _ApplicationState; }
            set { _ApplicationState = value; }
        }

        /// <summary>
        /// The possible list items for this menu.
        /// </summary>
        protected List<ListMenuOption> ListMenuOptions { get; set; } = new List<ListMenuOption>();

        /// <summary>
        /// Create a new List menu.
        /// </summary>
        /// <param name="appState">Current shared application state.</param>
        public CliMenu(ApplicationData.State appState)
        {
            this.ApplicationState = appState;
            this.MenuController = appState.MenuController;
        }

        /// <summary>
        /// Adds a new menu to the menu stack.
        /// </summary>
        /// <param name="menu">The menu to be added.</param>
        protected void MenuAdd(IMenu menu)
        {
            this.MenuController.Push(menu);
        }

        /// <summary>
        /// Exits this menu.
        /// </summary>
        public void MenuExit()
        {
            this.MenuController.Pop();
        }

        /// <summary>
        /// Adds a new list item to this list menu.
        /// </summary>
        /// <param name="name">The name to display for the list item.</param>
        /// <param name="key">The key to press to access this list item.</param>
        /// <param name="createMenu">The function to instantiate the menu.</param>
        protected void AddListMenuOption(String name, ConsoleKey key, Func<IMenu> createMenu)
        {
            this.ListMenuOptions.Add(new ListMenuOption { Name = name, Key = key, CreateListMenu = createMenu });
        }
        protected void AddListMenuSpace()
        {
            this.ListMenuOptions.Add(new ListMenuOption { Name = null, Key = ConsoleKey.NoName, CreateListMenu = null });
        }

        /// <summary>
        /// Attempts to navigate to a list item given the input.
        /// </summary>
        /// <param name="cki">The key pressed by the user.</param>
        /// <returns>TryNavigateResult indicating how the keypress was processed.</returns>
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

        /// <summary>
        /// Prints out all the list items in this list menu.
        /// </summary>
        /// <param name="title">The title of this list menu.</param>
        protected void PrintListMenu(string title)
        {
            Console.Clear();
            CliPrinter.Title(title);
            foreach (var entry in this.ListMenuOptions)
            {
                if (entry.Name == null) Console.Write("\n");
                else Console.WriteLine($"{(char)entry.Key}.  {entry.Name}");
            }
        }

        /// <summary>
        /// Input loop when running a List menu.
        /// </summary>
        /// <param name="printMenu">Function to print out the List menu.</param>
        protected virtual void RunListMenu(Action printMenu)
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

    /// <summary>
    /// Handles menu navigation and tracks the menu stack.
    /// </summary>
    public class MenuController
    {
        /// <summary>
        /// Menu stack containing all currently loaded menus.
        /// </summary>
        public Stack<IMenu> Menus { get; } = new Stack<IMenu>();

        /// <summary>
        /// Global shared application state.
        /// </summary>
        public ApplicationData.State AppState { get; set; }

        /// <summary>
        /// Add a new menu to navigate to.
        /// </summary>
        /// <param name="menu">The menu to navigate to.</param>
        public void Push(IMenu menu)
        {
            this.Menus.Push(menu);
        }

        /// <summary>
        /// Navigate back one level.
        /// </summary>
        public void Pop()
        {
            this.Menus.Pop();
        }

        /// <summary>
        /// Create a MenuController with the provided global application state.
        /// </summary>
        /// <param name="appState">Global shared applicatino state.</param>
        public MenuController(ApplicationData.State appState)
        {
            this.AppState = appState;
        }

        /// <summary>
        /// Starts the menu loop.
        /// </summary>
        public void Run()
        {
            try
            {
                Push(new StoreCliMenu.Main(this.AppState));
            }
            catch (Exception)
            {
                CliPrinter.Error("An error occurred while connecting to database. Please try running the program again.");
                return;
            }

            while (this.Menus.Count > 0)
            {
                try
                {
                    this.Menus.Peek().PrintMenu();
                    this.Menus.Peek().InputLoop();
                }
                catch (Exception)
                {
                    CliInput.PressAnyKey("\nAn error occurred while processing your request. Returning.");
                    this.Pop();
                    continue;
                }
            }
        }
    }
}