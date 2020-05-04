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
        private Nullable<DateTime> _TimeCreated;

        public virtual Nullable<DateTime> TimeCreated
        {
            get { return _TimeCreated; }
            set { _TimeCreated = value; }
        }

        private Nullable<DateTime> _TimeSubmitted;
        public virtual Nullable<DateTime> TimeSubmitted
        {
            get { return _TimeSubmitted; }
            set { _TimeSubmitted = value; }
        }

        private Nullable<DateTime> _TimeFulfilled;
        public virtual Nullable<DateTime> TimeFulfilled
        {
            get { return _TimeFulfilled; }
            set { _TimeFulfilled = value; }
        }
        
        public Nullable<double> AmountPaid { get; set; }

        private List<OrderLineItem> _OrderLineItems = new List<OrderLineItem>();
        public virtual List<OrderLineItem> OrderLineItems
        {
            get { return _OrderLineItems; }
            private set { _OrderLineItems = value; }
        }
        
        public Order(){
            this.OrderId = Guid.NewGuid();
            this.TimeCreated = DateTime.Now;
        }

        public Order(Customer customer, Location location)
        {
            this.OrderId = Guid.NewGuid();
            this.Customer = customer;
            this.Location = location;
            this.TimeCreated = DateTime.Now;
        }

        public void AddLineItem(OrderLineItem lineItem)
        {
            bool updated = false;
            foreach(var li in this.OrderLineItems)
            {
                if (li.Product == lineItem.Product)
                {
                    li.Quantity += lineItem.Quantity;
                    updated = true;
                    break;
                }
            }

            if (!updated) this.OrderLineItems.Add(lineItem);
        }
    }

    public class OrderLineItem
    {
        public Guid OrderLineItemId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Nullable<double> AmountCharged { get; set; }
        public int Quantity { get; set; }

        public OrderLineItem(){}
        public OrderLineItem(Order order, Product product)
        {
            this.OrderLineItemId = Guid.NewGuid();
            this.Order = order;
            this.Product = product;
        }
    }
}