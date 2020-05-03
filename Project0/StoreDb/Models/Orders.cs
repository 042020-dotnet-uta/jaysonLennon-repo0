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
        public double AmountPaid { get; set; }
        private List<OrderLineItem> _OrderLineItems = new List<OrderLineItem>();
        public virtual List<OrderLineItem> OrderLineItems
        {
            get { return _OrderLineItems; }
            set { _OrderLineItems = value; }
        }
        
        public Order(){}
        public Order(Customer customer, Location location)
        {
            this.OrderId = Guid.NewGuid();
            this.Customer = customer;
            this.Location = location;
        }

        public void AddLineItem(OrderLineItem lineItem)
        {
            this.OrderLineItems.Add(lineItem);
        }
    }

    public class OrderLineItem
    {
        public Guid OrderLineItemId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public double AmountCharged { get; set; }
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