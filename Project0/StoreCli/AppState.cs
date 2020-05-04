using System;
using Util;
using Microsoft.EntityFrameworkCore;
using StoreDb;
using StoreExtensions;

namespace ApplicationData
{
    public class State
    {
        public Nullable<Guid> OperatingLocationId { get; set; }
        public Nullable<Guid> CustomerId { get; set; }
        public DbContextOptions<StoreContext> DbOptions { get; set; }
        public MenuController MenuController { get; set; }
        public Nullable<Guid> CurrentOrderId { get; set; }

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

        public void RefreshDefaultLocation()
        {
            this.OperatingLocationId = this.DbOptions.GetDefaultLocation(this.CustomerId);
        }
    }
}