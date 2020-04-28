using System;
using System.Collections.Generic;

namespace StoreDb
{
    public class Order
    {
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        
        private Customer customer;
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        private Location location;
        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

        // The time the order was initially created.
        private DateTime timeCreated;
        public DateTime TimeCreated
        {
            get { return timeCreated; }
            set { timeCreated = value; }
        }
        // The time the order was sent for processing.
        private DateTime timeAccepted;
        public DateTime TimeAccepted
        {
            get { return timeAccepted; }
            set { timeAccepted = value; }
        }

        private DateTime timeFulfilled;
        public DateTime TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
    }
}
