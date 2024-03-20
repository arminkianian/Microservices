using ApplicationService.People.CommandHandlers;
using ApplicationService.People.Commands;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zamin.Core.Domain.ValueObjects;

namespace IntegrationTests.ApplicationService.People
{
    public class CreateNewPersonCommandHandlerTests : IClassFixture<PersonCommandRepositoryFixture>
    {
        private readonly PersonCommandRepositoryFixture _personCommandRepositoryFixture;

        public CreateNewPersonCommandHandlerTests(PersonCommandRepositoryFixture personCommandRepositoryFixture)
        {
            _personCommandRepositoryFixture = personCommandRepositoryFixture;
        }

        [Fact]
        public void when_pass_valid_input_value_expect_person_create_and_register_in_database()
        {
            //Arrange
            var command = new CreateNewPersonCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "Armin",
                LastName = "Kianian"
            };

            var commandHandler = new CreateNewPersonCommandHandler(_personCommandRepositoryFixture.PersonCommandRepository);

            //Act
            commandHandler.Handle(command);

            //Assert
            var person= _personCommandRepositoryFixture.Context.People.FirstOrDefault(p => p.BusinessId == BusinessId.FromGuid(command.Id));

            person.ShouldNotBeNull();
            person.FirstName.ShouldBe(command.FirstName);
            person.LastName.ShouldBe(command.LastName);
        }
    }
}
