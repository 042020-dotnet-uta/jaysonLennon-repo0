using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StoreDb {
    public class StoreContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationInventory> LocationInventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        public StoreContext(){}
        public StoreContext(DbContextOptions<StoreContext> options) : base (options) {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) options.UseSqlite("Data Source=storeapp.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<LocationInventory>()
                .HasKey(e => new { e.LocationId, e.InventoryItemId });

            modelBuilder.Entity<Product>()
                .Ignore(e => e.MaxOrderOverride);

            modelBuilder.Entity<ProductComponent>()
                .HasKey(e => new { e.ProductId, e.InventoryItemId });

            modelBuilder.Entity<OrderProduct>()
                .HasKey(e => new { e.OrderId, e.ProductId });

            modelBuilder.Entity<Promotion>()
                .HasKey(e => new { e.PromotionId, e.LocationId });
            
            modelBuilder.Entity<Promotion>()
                .Property(p => p.DiscountType)
                .HasConversion(new EnumToStringConverter<DiscountType>());
        }
    }
}