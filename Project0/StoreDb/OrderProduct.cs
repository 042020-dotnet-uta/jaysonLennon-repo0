using System;
using System.Collections.Generic;

namespace StoreDb
{
    public class OrderProduct
    {
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int productQuantity;
        public int ProductQuantity
        {
            get { return productQuantity; }
            set { productQuantity = value; }
        }
    }
}
