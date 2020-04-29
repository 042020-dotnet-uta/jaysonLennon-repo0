using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public virtual ProductDetail Detail { get; set; }

        private Product(){}
        public Product(string name)
        {
            this.Detail = new ProductDetail { Name = name };
        }
    }

    public class ProductDetail
    {
        public Guid ProductDetailId { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}