namespace StoreDb
{
    public class InventoryItem
    {
        private int inventoryItemId;
        public int InventoryItemId
        {
            get { return inventoryItemId; }
            set { inventoryItemId = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
