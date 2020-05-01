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

        private Product(){}
        public Product(string name) { }
    }
}