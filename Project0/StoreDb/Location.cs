using System.Collections.Generic;

namespace StoreDb
{
    public class Location
    {
        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private List<LocationInventory> inventory;
        public List<LocationInventory> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }
    }
}

