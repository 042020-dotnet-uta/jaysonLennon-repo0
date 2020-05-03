using System;
using Util;
using Microsoft.EntityFrameworkCore;
using StoreDb;
using StoreExtensions;

namespace ApplicationData
{
    public class State
    {
        private Nullable<Guid> _OperatingLocationId;
        public Nullable<Guid> OperatingLocationId
        {
            get { return _OperatingLocationId; }
            set { _OperatingLocationId = value; }
        }

        private Nullable<Guid> _CustomerId;
        public Nullable<Guid> CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }

        private DbContextOptions<StoreContext> _DbOptions;
        public DbContextOptions<StoreContext> DbOptions
        {
            get { return _DbOptions; }
            set { _DbOptions = value; }
        }

        private MenuController _MenuController;
        public MenuController MenuController
        {
            get { return _MenuController; }
            set { _MenuController = value; }
        }

        private Nullable<Guid> _CurrentOrderId;
        public Nullable<Guid> CurrentOrderId
        {
            get { return _CurrentOrderId; }
            set { _CurrentOrderId = value; }
        }

        public void RefreshCurrentOrder()
        {
            using (var db = new StoreContext(this.DbOptions))
            {
                var currentOrder = db.FindCurrentOrder(this.CustomerId);
                if (currentOrder != null)
                {
                    this._CurrentOrderId = currentOrder.OrderId;
                }
            }
        }

        public void RefreshDefaultLocation()
        {
            this._OperatingLocationId = this.DbOptions.GetDefaultLocation(this.CustomerId);
        }
    }
}