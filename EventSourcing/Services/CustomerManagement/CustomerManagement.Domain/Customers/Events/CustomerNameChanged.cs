using Framework.Core;

namespace CustomerManagement.Domain.Customers.Events
{
    public class CustomerNameChanged : IDomainEvent
    {
        public string CustomerId { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public CustomerNameChanged(string customerId, string firstName, string lastname)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastname;
        }
    }
}