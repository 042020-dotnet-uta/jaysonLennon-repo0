using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace Util
{
    class Program
    {
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