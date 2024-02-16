using CustomerManagement.Domain.Customers;
using CustomerManagement.Domain.Customers.Services;
using Framework.Core;

namespace CustomerManagement.Infrastructure.Persistence.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IEventStore _eventStore;

        public CustomersRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            var customerEvents = await _eventStore.LoadAsync(id, typeof(Customer).Name);
            return new Customer(customerEvents);
        }

        public async Task<Guid> SaveAsync(Customer customer)
        {
            await _eventStore.SaveAsync(customer.Id, nameof(Customer), customer.Version, customer.DomainEvents);
            return customer.Id;
        }
    }
}
