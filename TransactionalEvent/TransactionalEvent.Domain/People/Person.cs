using System.ComponentModel;

namespace TransactionalEvent.Domain.People
{
    public class Person : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsMale { get; private set; }

        public Person()
        {

        }

        public Person(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;

            AddEvent(new PersonCreatedEvent()
            {
                FirstName = firstName,
                LastName = lastName
            });
        }

        public void SetBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;

            AddEvent(new BirthDateUpdatedEvent()
            {
                Id = this.Id,
                BirthDate = birthDate
            });
        }
    }
}
