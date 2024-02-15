namespace CustomerManagement.Domain.Customers.Services
{
    public interface ICustomersRepository
    {
        Task<Guid> SaveAsync(Customer customer);
        Task<Customer> GetCustomer(Guid id);
    }
}
