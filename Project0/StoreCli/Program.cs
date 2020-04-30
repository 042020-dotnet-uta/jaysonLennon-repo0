using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDb;

namespace StoreCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseSqlite("Data Source=store.sqlite")
                .UseLazyLoadingProxies()
                .Options;

            var menu = new MenuController(options);
            menu.Run();
        }
    }
}