
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreDb;
using System;
using StoreExtensions;

namespace TestStoreDb
{
    public class TestCustomer
    {
        [Fact]
        public void SearchesForCustomerByFirstName()
        {
            var options = TestUtil.GetMemDbOptions("SearchesForCustomerByFirstName");

            using (var db = new StoreContext(options))
            {
                var customer1 = new Customer("test customer 1");
                db.Add(customer1);

                var customer2 = new Customer("test2");
                db.Add(customer2);

                var customer3 = new Customer("test customer 3");
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByFirstName("Customer 1");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.FirstName, findCustomer1.First().FirstName);

                var findCustomer2 = db.FindCustomerByFirstName("test2");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.FirstName, findCustomer2.First().FirstName);

                var findCustomers = db.FindCustomerByFirstName("st cu");
                Assert.Equal(2, findCustomers.Count());
                Assert.Equal(customer1.FirstName, findCustomers
                                                .Where(c => c.FirstName == customer1.FirstName)
                                                .First()
                                                .FirstName);
                Assert.Equal(customer3.FirstName, findCustomers
                                                .Where(c => c.FirstName == customer3.FirstName)
                                                .First()
                                                .FirstName);
            }
        }

        [Fact]
        public void SearchesForCustomerByLastName()
        {
            var options = TestUtil.GetMemDbOptions("SearchesForCustomerByLastName");

            using (var db = new StoreContext(options))
            {
                var customer1 = new Customer("test customer 1");
                customer1.LastName = "c1 last name";
                db.Add(customer1);

                var customer2 = new Customer("test2");
                customer2.LastName = "c2";
                db.Add(customer2);

                var customer3 = new Customer("test customer 3");
                customer3.LastName = "c3 last name";
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByLastName("1");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.LastName, findCustomer1.First().LastName);

                var findCustomer2 = db.FindCustomerByLastName("c2");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.LastName, findCustomer2.First().LastName);

                var findCustomers = db.FindCustomerByLastName("last");
                Assert.Equal(2, findCustomers.Count());
                Assert.Equal(customer1.LastName, findCustomers
                                                .Where(c => c.LastName == customer1.LastName)
                                                .First()
                                                .LastName);
                Assert.Equal(customer3.LastName, findCustomers
                                                .Where(c => c.LastName == customer3.LastName)
                                                .First()
                                                .LastName);
            }
        }

        [Fact]
        public void SearchesForCustomerByName()
        {
            var options = TestUtil.GetMemDbOptions("SearchesForCustomerByName");

            using (var db = new StoreContext(options))
            {
                var customer1 = new Customer("test customer 1");
                customer1.LastName = "c1 last name";
                db.Add(customer1);

                var customer2 = new Customer("test2");
                customer2.LastName = "c2";
                db.Add(customer2);

                var customer3 = new Customer("test customer 3");
                customer3.LastName = "c3 last name";
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByName("1");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.LastName, findCustomer1.First().LastName);

                var findCustomer2 = db.FindCustomerByName("c2");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.LastName, findCustomer2.First().LastName);

                var findCustomers = db.FindCustomerByName("cust");
                Assert.Equal(2, findCustomers.Count());
                Assert.Equal(customer1.LastName, findCustomers
                                                .Where(c => c.LastName == customer1.LastName)
                                                .First()
                                                .LastName);
                Assert.Equal(customer3.LastName, findCustomers
                                                .Where(c => c.LastName == customer3.LastName)
                                                .First()
                                                .LastName);
            }
        }
    }
}
