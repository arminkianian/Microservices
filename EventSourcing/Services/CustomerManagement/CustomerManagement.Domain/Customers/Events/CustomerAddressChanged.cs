using Framework.Core;

namespace CustomerManagement.Domain.Customers.Events
{
    public class CustomerAddressChanged : IDomainEvent
    {
        public CustomerAddressChanged(string customerId, string street, string city, string country, string zipCode)
        {
            CustomerId = customerId;
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        public string CustomerId { get; }
        public string Street { get; }
        public string City { get; }
        public string Country { get; }
        public string ZipCode { get; }
    }
}