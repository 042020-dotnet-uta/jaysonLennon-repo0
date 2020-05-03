using System;
using Util;
using Microsoft.EntityFrameworkCore;
using StoreDb;

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

        private Order _CurrentOrder;
        public Order CurrentOrder
        {
            get { return _CurrentOrder; }
            set { _CurrentOrder = value; }
        }
        
        
    }

}