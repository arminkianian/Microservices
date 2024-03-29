using ApplicationService.People.CommandHandlers;
using ApplicationService.People.Commands;
using Domain.People.Entity;
using Domain.People.Repositories;
using Microsoft.AspNetCore.Mvc;
using Zamin.Core.Domain.ValueObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IPersonCommandRepository repository, [FromQuery] Guid id)
        {
            _logger.LogInformation($"Executed at {DateTime.Now}");

            Person person = repository.Get(BusinessId.FromGuid(id));

            return Ok(new
            {
                person.FirstName,
                person.LastName,
                Id = person.BusinessId.Value
            });
        }

        [HttpPost]
        public IActionResult Post([FromServices] CreateNewPersonCommandHandler handler,[FromBody]CreateNewPersonCommand command)
        {
            handler.Handle(command);
            return Ok();
        }
    }
}
