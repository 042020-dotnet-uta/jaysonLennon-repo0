using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreDb;

namespace TestStoreDb
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(databaseName: "wtf")
            .Options;

            using (var db = new StoreContext(options))
            {
                db.InventoryItems.Add(new InventoryItem { InventoryItemId = 1, Name = "test" });
                db.Locations.Add(new Location { LocationId = 1, Name = "location", Address = "123" });
                db.SaveChanges();
            }

            using (var db = new StoreContext(options))
            {
                Location loc = db.Locations.Where(i => i.LocationId == 1).ToList()[0];
            }
        }
    }
}
