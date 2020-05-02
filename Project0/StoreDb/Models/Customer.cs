using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public class Customer
    {
        public Guid CustomerId { get; private set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public virtual Location DefaultLocation { get; set; }

        public Customer(){}

        public Customer(string firstName)
        {
            this.CustomerId = Guid.NewGuid();
            this.FirstName = firstName;
        }
    }
}