using Zamin.Core.Domain.Events;

namespace Domain.People.Events
{
    public class PersonCreatedEvent : IDomainEvent
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Id { get; }

        public PersonCreatedEvent(Guid id, string firstName, string lastName)
        {
            Id = id.ToString();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}