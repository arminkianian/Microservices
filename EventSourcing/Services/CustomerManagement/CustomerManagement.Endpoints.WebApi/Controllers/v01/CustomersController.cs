using CustomerManagement.ApplicationService.Customers;
using CustomerManagement.ApplicationService.Customers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Endpoints.WebApi.Controllers.v01
{
    [Route("api/v01/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customersService;

        public CustomersController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpPost]
        public async Task<object> CreateCustomer([FromBody] SaveCustomerDto customer)
        {
            var insertedCustomerId = await _customersService.CreateCustomer(customer.FirstName, customer.LastName);
            return new { PersonId = insertedCustomerId.ToString() };
        }

        [HttpPost]
        public async Task UpdateCustomer([FromQuery] string customerId, [FromBody] SaveCustomerDto customer)
        {
            await _customersService.UpdateCustomer(customerId, customer.FirstName, customer.LastName);
            Ok();
        }

        [HttpPost]
        public async Task UpdateAddress([FromQuery] string customerId, [FromBody] AddressDto address)
        {
            await _customersService.UpdateAddress(customerId, address.Street, address.City, address.Country, address.ZipCode);
            Ok();
        }

        [HttpGet]
        public async Task<CustomerDto> GetCustomer([FromQuery] string customerId)
        {
            return await _customersService.GetCustomer(customerId);
        }
    }
}
