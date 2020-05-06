using System;
using StoreCliUtil;
using Microsoft.EntityFrameworkCore;
using StoreDb;
using StoreExtensions;

/// <summary>
/// Global application classes.
/// </summary>
namespace ApplicationData
{
    /// <summary>
    /// State data and required by various user menus.
    /// </summary>
    public class UserState
    {
        private State GlobalState { get; set; }

        /// <summary>
        /// The location to associate with actions.
        /// </summary>
        public Nullable<Guid> OperatingLocationId { get; set; }

        /// <summary>
        /// The location name selected by the user (cache only).
        /// </summary>
        /// <value></value>
        public string OperatingLocationName { get; set; }

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
                var location = db.GetLocationById(this.OperatingLocationId);
                var currentOrder = db.FindCurrentOrder(location, this.CustomerId);
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
        /// Retrieves the default location for the customer.
        /// </summary>
        public void RefreshDefaultLocation()
        {
            this.OperatingLocationId = this.GlobalState.DbOptions.GetDefaultLocation(this.CustomerId);
            using (var db = new StoreDb.StoreContext(this.GlobalState.DbOptions))
            {
                var location = db.GetLocation(this.OperatingLocationId);
                this.OperatingLocationName = location.Name;
            }
        }

        /// <summary>
        /// Create new instance of UserState.
        /// </summary>
        /// <param name="globalState">Global state being used by the program.</param>
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

        /// <summary>
        /// The state required by User menus.
        /// </summary>
        public UserState UserData { get; set; }

        /// <summary>
        /// Create new instance of State.
        /// </summary>
        public State() {
            this.UserData = new UserState(this);
        }
    }
}