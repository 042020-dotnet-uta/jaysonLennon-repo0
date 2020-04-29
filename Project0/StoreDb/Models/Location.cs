using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Location
    {
        public Guid LocationId { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<LocationInventory> Inventory { get; } = new List<LocationInventory>();
    }

    public class LocationInventory
    {
        public Guid LocationInventoryId { get; private set; }
        public virtual Product Product { get; set; }
        public virtual Location Location { get; set; }

        public int Quantity { get; private set; }

        private LocationInventory(){}
        public LocationInventory(Product product, Location location)
        {
            this.Product = product;
            this.Location = location;
        }
    }
}