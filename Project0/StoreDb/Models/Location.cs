using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Location
    {
        public Guid LocationId { get; private set; }
        public string Name { get; private set; }
        public virtual Address Address { get; private set; }

        public Location() { }
        public Location(string name)
        {
            this.LocationId = Guid.NewGuid();
            this.Name = name;
        }
    }

    public class LocationInventory
    {
        public Guid LocationInventoryId { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual Location Location { get; private set; }

        public int Quantity { get; private set; }

        public LocationInventory() { }
        public LocationInventory(Product product, Location location, int quantity)
        {
            this.LocationInventoryId = Guid.NewGuid();
            this.Product = product;
            this.Location = location;
            this.Quantity = quantity;
        }

        // Attempts to adjust the quantity of this inventory item.
        // If there is an insufficient quantity of items, then
        // no adjustment will take place.
        //
        // If no adjustment occurs, then a null is returned.
        // Otherwise, the amount remaining in stock will be returned.
        public Nullable<int> TryAdjustQuantity(int amount)
        {
            if (this.Quantity + amount < 0)
            {
                return null;
            }
            else
            {
                this.Quantity += amount;
                return this.Quantity;
            }
        }
    }
}