using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalEvent.Dal;
using TransactionalEvent.Domain.People;

namespace TransactionalEvent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonDbContext dbContext;

        public PeopleController(PersonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public IActionResult All()
        {
            return Ok(dbContext.People.ToList());
        }

        [HttpGet("[action]")]
        public IActionResult Create(string firstName, string lastName)
        {
            Person person = new Person(Guid.NewGuid(), firstName, lastName);

            this.dbContext.People.Add(person);

            this.dbContext.SaveChanges();

            return Ok();
        }
    }
}
