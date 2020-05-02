using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

        public Product(){}
        public Product(string name) {
            this.ProductId = Guid.NewGuid();
            this.Name = name;
        }
        public Product(string name, double price) {
            this.ProductId = Guid.NewGuid();
            this.Name = name;
            this.Price = price;
        }
    }
}