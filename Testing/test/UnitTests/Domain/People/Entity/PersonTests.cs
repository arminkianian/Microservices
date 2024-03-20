using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.People.Entity;
using Zamin.Core.Domain.ValueObjects;
using Shouldly;
using Domain.People.Events;
using Domain.People.Exceptions;

namespace UnitTests.Domain.People.Entity
{
    public class PersonTests
    {
        [Fact]
        public void when_pass_correct_values_to_factory_expect_person_created()
        {
            //arrange
            var firstName = "Armin";
            var lastName = "Kianian";
            var id = BusinessId.FromGuid(Guid.NewGuid());


            //action
            var person = Person.Create(id, firstName, lastName);

            //assert
            person.ShouldNotBeNull();
            person.BusinessId.ShouldNotBeNull();
            person.BusinessId.ShouldBe(id);

            person.FirstName.ShouldBe(firstName);
            person.LastName.ShouldBe(lastName);


            person.GetEvents().Count().ShouldBe(1);
            var @event = person.GetEvents().FirstOrDefault();
            @event.ShouldBeOfType<PersonCreatedEvent>();
        }

        [Fact]
        public void when_pass_invalid_bussiness_id_to_factory_expect_invalid_person_is_exception()
        {
            //Arrange
            var firstName = "Armin";
            var lastName = "Kianian";
            BusinessId id = null;


            //Act
            var invalidPersonIdException = Record.Exception(() =>
                Person.Create(id, firstName, lastName)
            );

            //Assers
            invalidPersonIdException.ShouldNotBeNull();
            invalidPersonIdException.ShouldBeOfType<InvalidPersonIdException>();

        }
    }
}
