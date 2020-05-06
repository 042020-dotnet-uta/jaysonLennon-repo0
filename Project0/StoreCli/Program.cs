using Microsoft.EntityFrameworkCore;
using StoreDb;
using StoreCliUtil;

namespace StoreCli
{
    /// <summary>
    /// Main program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">CLI args currently not implemented.</param>
        static void Main(string[] args)
        {
            var dbOptions = new DbContextOptionsBuilder<StoreContext>()
                .UseSqlite("Data Source=store.sqlite")
                .UseLazyLoadingProxies()
                .Options;

            var state = new ApplicationData.State();
            state.DbOptions = dbOptions;
            state.MenuController = new MenuController(state);

            state.MenuController.Run();
        }
    }
}