using System;
using System.Linq;
using System.Security.Cryptography;

namespace StoreDb
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Address Address { get; set; }
        public string PhoneNumber { get; set; }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = Util.Hash.Sha256(value); }
        }

        public virtual Location DefaultLocation { get; set; }

        public Customer(){}

        public Customer(string firstName)
        {
            this.CustomerId = Guid.NewGuid();
            this.FirstName = firstName;
        }
    }
}