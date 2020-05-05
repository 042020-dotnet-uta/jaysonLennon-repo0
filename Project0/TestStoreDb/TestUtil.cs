using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using StoreDb;

namespace TestStoreDb
{
    public class TestUtil
    {
        public static DbContextOptions<StoreContext> GetMemDbOptions(string dbName)
        {
            var sqlite = new SqliteConnection("Filename=:memory:");
            sqlite.Open();
            var options = new DbContextOptionsBuilder<StoreContext>()
                       //.UseInMemoryDatabase(databaseName: dbName)
                       .UseSqlite(sqlite)
                       .UseLazyLoadingProxies()
                       .Options;

            using (var db = new StoreContext(options))
            {
                db.Database.Migrate();
            }

            return options;
        }
    }

}
