using System.ComponentModel.DataAnnotations;

namespace StoreDb
{
    public class LocationInventory
    {
        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        private int inventoryItemId;
        public int InventoryItemId
        {
            get { return inventoryItemId; }
            set { inventoryItemId = value; }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
