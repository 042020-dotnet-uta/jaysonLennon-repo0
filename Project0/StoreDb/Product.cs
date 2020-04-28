using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDb
{
    public class Product
    {
        
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int maxOrder;
        public int MaxOrder
        {
            get { return maxOrder; }
            set { maxOrder = value; }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private bool maxOrderOverride;
        public bool MaxOrderOverride
        {
            get { return maxOrderOverride; }
            set { maxOrderOverride = value; }
        }
        
    }
}
