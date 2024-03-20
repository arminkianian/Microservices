using ApplicationService.People.CommandHandlers;
using ApplicationService.People.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Domain.People.Repositories;
using Domain.People.Entity;

namespace UnitTests.ApplicationService
{
    public class CreateNewPersonCommandHandlerTests
    {
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

            var moqPersonRepository = new Mock<IPersonCommandRepository>();
            moqPersonRepository.Setup(c => c.Add(It.IsAny<Person>()));

            var commandHandler = new CreateNewPersonCommandHandler(moqPersonRepository.Object);

            //Act
            commandHandler.Handle(command);

            //Assert
            moqPersonRepository.Verify(mock => mock.Add(It.IsAny<Person>()), Times.Once());
        }
    }
}
