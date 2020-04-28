using System;
using System.Collections.Generic;

namespace StoreDb
{
    public class ProductComponent
    {
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int inventoryItemId;
        public int InventoryItemId
        {
            get { return inventoryItemId; }
            set { inventoryItemId = value; }
        }

        private int inventoryItemQuantity;
        public int InventoryItemQuantity
        {
            get { return inventoryItemQuantity; }
            set { inventoryItemQuantity = value; }
        }
        
    }
}
