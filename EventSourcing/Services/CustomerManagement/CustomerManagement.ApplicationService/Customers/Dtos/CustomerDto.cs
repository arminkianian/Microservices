namespace CustomerManagement.ApplicationService.Customers.Dtos
{
    public class CustomerDto
    {
        public Guid customerId { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public AddressDto Address { get; internal set; }
    }
}
