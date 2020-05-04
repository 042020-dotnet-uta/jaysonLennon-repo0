using System;
using Util;
using Microsoft.EntityFrameworkCore;
using StoreDb;
using StoreExtensions;

/// <summary>
/// Global application classes.
/// </summary>
namespace ApplicationData
{
    /// <summary>
    /// State that needs to be shared across the application.
    /// </summary>
    public class State
    {
        //TODO: Separate user state into its own class.
        /// <summary>
        /// (In User menus) the location to associate with actions.
        /// </summary>
        public Nullable<Guid> OperatingLocationId { get; set; }

        /// <summary>
        /// (In User menus) the customer ID to associate with actions.
        /// </summary>
        public Nullable<Guid> CustomerId { get; set; }

        /// <summary>
        /// (In User menus) the order ID to associate with actions.
        /// </summary>
        public Nullable<Guid> CurrentOrderId { get; set; }

        /// <summary>
        /// Database options.
        /// </summary>
        public DbContextOptions<StoreContext> DbOptions { get; set; }

        /// <summary>
        /// The list menu controller.
        /// </summary>
        public MenuController MenuController { get; set; }

        /// <summary>
        /// (In User menus) Retrieves the current open order for the customer.
        /// </summary>
        public void RefreshCurrentOrder()
        {
            using (var db = new StoreContext(this.DbOptions))
            {
                var currentOrder = db.FindCurrentOrder(this.CustomerId);
                if (currentOrder != null)
                {
                    this.CurrentOrderId = currentOrder.OrderId;
                }
                else
                {
                    this.CurrentOrderId = null;
                }
            }
        }

        /// <summary>
        /// (In User menus) RRetrieves the default location for the customer.
        /// </summary>
        public void RefreshDefaultLocation()
        {
            this.OperatingLocationId = this.DbOptions.GetDefaultLocation(this.CustomerId);
        }
    }
}