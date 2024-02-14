using CustomerManagement.Domain.Customers.Events;
using Framework.Core;

namespace CustomerManagement.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }


        public Customer() { }

        public Customer(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        public static Customer CreateCustomer(string firstName, string lastName)
        {
            // business logic
            var customer = new Customer();
            customer.Apply(new CustomerCreated(Guid.NewGuid().ToString(), firstName, lastName));
            return customer;
        }

        public void On(CustomerCreated @event)
        {
            Id = Guid.Parse(@event.CustomerId);
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        public void ChangeName(string firstName, string lastname)
        {
            Apply(new CustomerNameChanged(Id.ToString(), firstName, lastname));
        }

        public void On(CustomerNameChanged @event)
        {
            Id = Guid.Parse(@event.CustomerId);
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        public void ChangeAddress(string street, string city, string country, string zipCode)
        {
            Apply(new CustomerAddressChanged(Id.ToString(), street, city, country, zipCode));
        }

        public void On(CustomerAddressChanged @event)
        {
            Address = new Address
            {
                Street = @event.Street,
                City = @event.City,
                Country = @event.Country,
                ZipCode = @event.ZipCode
            };
        }
    }
}
