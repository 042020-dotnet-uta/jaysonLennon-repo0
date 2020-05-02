
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
                var customer1 = new Customer("--z6HkRe" + Guid.NewGuid().ToString());
                db.Add(customer1);

                var customer2 = new Customer("dPmth" + Guid.NewGuid().ToString());
                db.Add(customer2);

                var customer3 = new Customer("--za/tvV" + Guid.NewGuid().ToString());
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByFirstName("6HkRe");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.FirstName, findCustomer1.First().FirstName);

                var findCustomer2 = db.FindCustomerByFirstName("Pmth");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.FirstName, findCustomer2.First().FirstName);

                var findCustomers = db.FindCustomerByFirstName("--z");
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
                var customer1 = new Customer("" + Guid.NewGuid().ToString());
                customer1.LastName = "z--bfS/qc" + Guid.NewGuid().ToString();
                db.Add(customer1);

                var customer2 = new Customer("" + Guid.NewGuid().ToString());
                customer2.LastName = "1XfcnY" + Guid.NewGuid().ToString();
                db.Add(customer2);

                var customer3 = new Customer("" + Guid.NewGuid().ToString());
                customer3.LastName = "z--zB8tkm" + Guid.NewGuid().ToString();
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByLastName("bfs");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.LastName, findCustomer1.First().LastName);

                var findCustomer2 = db.FindCustomerByLastName("cn");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.LastName, findCustomer2.First().LastName);

                var findCustomers = db.FindCustomerByLastName("z--");
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
                var customer1 = new Customer("7/Yf" + Guid.NewGuid().ToString());
                customer1.LastName = "q/wB" + Guid.NewGuid().ToString();
                db.Add(customer1);

                var customer2 = new Customer("nNg63" + Guid.NewGuid().ToString());
                customer2.LastName = "ZQVkE" + Guid.NewGuid().ToString();
                db.Add(customer2);

                var customer3 = new Customer("w6Ntm" + Guid.NewGuid().ToString());
                customer3.LastName = "I7/v2ZN" + Guid.NewGuid().ToString();
                db.Add(customer3);
                db.SaveChanges();

                var findCustomer1 = db.FindCustomerByName("wb");
                Assert.Equal(1, findCustomer1.Count());
                Assert.Equal(customer1.LastName, findCustomer1.First().LastName);

                var findCustomer2 = db.FindCustomerByName("Vk");
                Assert.Equal(1, findCustomer2.Count());
                Assert.Equal(customer2.LastName, findCustomer2.First().LastName);

                var findCustomers = db.FindCustomerByName("7/");
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
