namespace StoreCliUtil
{
    /// <summary>
    /// Must be implemented by all menus in order to be tracked by the menu controller.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Ran whenever the menu should be printed to the console.
        /// This method should only print out information based on the state of the menu.
        /// </summary>
        void PrintMenu();

        /// <summary>
        /// Ran whenever control of the application is given to this specific menu.
        /// This method should handle any input provided by the user. Returning
        /// from this method is an indication that the menu has been exited and
        /// it will be destroyed by the menu controller.
        /// </summary>
        void InputLoop();
    }
}