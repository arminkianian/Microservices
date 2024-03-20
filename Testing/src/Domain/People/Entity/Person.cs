using Domain.People.Events;
using Domain.People.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Domain.People.Entity
{
    public class Person : AggregateRoot
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private Person()
        {
        }

        public static Person Create(BusinessId id, string firstName, string lastName)
        {
            if (id == null)
            {
                throw new InvalidPersonIdException("PersonIdIsNull");
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new InvalidFirstNameException("FirstNameIsNull");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new InvalidLastNameException("LastNameIsNull");
            }

            var person = new Person()
            {
                BusinessId = id,
                FirstName = firstName,
                LastName = lastName
            };

            person.AddEvent(new PersonCreatedEvent(person.BusinessId.Value, person.FirstName, person.LastName));

            return person;
        }
    }
}
