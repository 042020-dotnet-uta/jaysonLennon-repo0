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
        public virtual List<ProductComponent> ProductComponents { get; set; } = new List<ProductComponent>();

        private Product(){}
        public Product(string name) { }
    }

    public class ProductComponent
    {
        public Guid ProductComponentId { get; set; }
        public string Name { get; set; }
    }
}