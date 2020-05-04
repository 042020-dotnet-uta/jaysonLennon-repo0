using System;
using System.Linq;
using System.Security.Cryptography;

namespace StoreDb
{
    public class Address
    {
        public Guid AddressId { get; private set; }
        public virtual AddressLine1 Line1 { get; set; }
        public virtual AddressLine2 Line2 { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual ZipCode Zip { get; set; }
        public Address(){}
    }

    public class AddressLine1
    {
        public Guid AddressLine1Id { get; private set; }
        public string Data { get; private set; }
        public AddressLine1(){}
    }

    public class AddressLine2
    {
        public Guid AddressLine2Id { get; private set; }
        public string Data { get; private set; }
        public AddressLine2(){}
    }

    public class City
    {
        public Guid CityId { get; private set; }
        public string Name { get; private set; }
        public City(){}
    }

    public class State
    {
        public Guid StateId { get; private set; }
        public string Name { get; private set; }
        public State(){}
    }

    public class ZipCode
    {
        public Guid ZipCodeId { get; private set; }
        public string Zip { get; private set; }
        public ZipCode(){}
    }
}