using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreDb;
using System;

namespace TestStoreDb
{
    public class TestApi
    {
        public static DbContextOptions<StoreContext> GetMemDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<StoreContext>()
                       .UseInMemoryDatabase(databaseName: dbName)
                       .Options;
        }
    }

}
