using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    /// <summary>
    /// Context used to access the database.
    /// </summary>
    public class StoreContext : DbContext
    {
        /// <summary>
        /// The Products table.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// The Customers table.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// The Locations table.
        /// </summary>
        public DbSet<Location> Locations { get; set; }
        /// <summary>
        /// The LocationInventories table.
        /// </summary>
        public DbSet<LocationInventory> LocationInventories { get; set; }
        /// <summary>
        /// The Orders table.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// The OrderLineItems table.
        /// </summary>
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        /// <summary>
        /// The Addresses table.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Needed for EF.
        /// </summary>
        public StoreContext() { }

        /// <summary>
        /// Create a new database context.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        /// <returns>A new database context.</returns>
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        /// <summary>
        /// Applies a default configuration it one has not yet been configured.
        /// </summary>
        /// <param name="options">Provided by EF.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options
                    .UseSqlite("Data Source=store.sqlite")
                    .UseLazyLoadingProxies();
        }

        /// <summary>
        /// Table configuration when creating the model.
        /// </summary>
        /// <param name="modelBuilder">Provided by EF.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Order>().Property(t => t.TimeCreated).IsRequired(false);
        }
    }
}