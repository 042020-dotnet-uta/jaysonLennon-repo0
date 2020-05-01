using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Location
    {
        public Guid LocationId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public virtual List<LocationInventory> Inventory { get; private set; } = new List<LocationInventory>();

        private Location(){}
        public Location(string name)
        {
            this.Name = name;
        }
    }

    public class LocationInventory
    {
        public Guid LocationInventoryId { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual Location Location { get; private set; }

        public int Quantity { get; private set; }

        private LocationInventory(){}
        public LocationInventory(Product product, Location location)
        {
            this.Product = product;
            this.Location = location;
        }
    }
}