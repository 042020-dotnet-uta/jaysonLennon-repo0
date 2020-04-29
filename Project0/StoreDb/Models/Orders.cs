using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public DateTime TimeFulfilled { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
    }

    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}