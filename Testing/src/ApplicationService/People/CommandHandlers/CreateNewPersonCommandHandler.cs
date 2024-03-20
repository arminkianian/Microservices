using ApplicationService.People.Commands;
using Domain.People.Entity;
using Domain.People.Repositories;
using Zamin.Core.Domain.ValueObjects;

namespace ApplicationService.People.CommandHandlers
{
    public class CreateNewPersonCommandHandler
    {
        private readonly IPersonCommandRepository personCommandRepository;

        public CreateNewPersonCommandHandler(IPersonCommandRepository personCommandRepository)
        {
            this.personCommandRepository = personCommandRepository;
        }

        public void Handle(CreateNewPersonCommand command)
        {
            var person = Person.Create(BusinessId.FromGuid(command.Id), command.FirstName, command.LastName);

            this.personCommandRepository.Add(person);
        }
    }
}
