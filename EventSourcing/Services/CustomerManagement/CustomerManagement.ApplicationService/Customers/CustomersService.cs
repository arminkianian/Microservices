using CustomerManagement.ApplicationService.Customers.Dtos;
using CustomerManagement.Domain.Customers;
using CustomerManagement.Domain.Customers.Services;

namespace CustomerManagement.ApplicationService.Customers
{
    public class CustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<CustomerDto> GetCustomer(string customerId)
        {
            var customer = await _customersRepository.GetCustomer(Guid.Parse(customerId));

            if (customer == null) return new CustomerDto();

            return new CustomerDto()
            {
                customerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address != null ? new AddressDto
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    Country = customer.Address.Country,
                    ZipCode = customer.Address.ZipCode,
                } : null,
            };
        }

        public Task<Guid> CreateCustomer(string firstName, string lastName)
        {
            var customer = Customer.CreateCustomer(firstName, lastName);
            return _customersRepository.SaveAsync(customer);
        }

        public async Task UpdateCustomer(string customerId, string firstName, string lastName)
        {
            var customer = await _customersRepository.GetCustomer(Guid.Parse(customerId));
            customer.ChangeName(firstName, lastName);
            await _customersRepository.SaveAsync(customer);
        }

        public async Task UpdateAddress(string customerId, string street, string city, string country, string zipcode)
        {
            var customer = await _customersRepository.GetCustomer(Guid.Parse(customerId));

            if (customer == null) return;

            customer.ChangeAddress(street, city, country, zipcode);
            await _customersRepository.SaveAsync(customer);
        }
    }
}
