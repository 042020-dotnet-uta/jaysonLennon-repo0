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
    public class UserState
    {
        private State GlobalState { get; set; }

        /// <summary>
        /// The location to associate with actions.
        /// </summary>
        public Nullable<Guid> OperatingLocationId { get; set; }

        /// <summary>
        /// The customer ID to associate with actions.
        /// </summary>
        public Nullable<Guid> CustomerId { get; set; }

        /// <summary>
        /// The order ID to associate with actions.
        /// </summary>
        public Nullable<Guid> CurrentOrderId { get; set; }

        /// <summary>
        /// Retrieves the current open order for the customer.
        /// </summary>
        public void RefreshCurrentOrder()
        {
            using (var db = new StoreContext(this.GlobalState.DbOptions))
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
            this.OperatingLocationId = this.GlobalState.DbOptions.GetDefaultLocation(this.CustomerId);
        }

        public UserState(State globalState)
        {
            this.GlobalState = globalState;
        }
    }

    /// <summary>
    /// State that needs to be shared across the application.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Database options.
        /// </summary>
        public DbContextOptions<StoreContext> DbOptions { get; set; }

        /// <summary>
        /// The list menu controller.
        /// </summary>
        public MenuController MenuController { get; set; }

        public UserState UserData { get; set; }

        public State() {
            this.UserData = new UserState(this);
        }
    }
}