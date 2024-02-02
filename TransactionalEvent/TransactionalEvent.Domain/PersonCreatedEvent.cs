namespace TransactionalEvent.Domain
{
    public class PersonCreatedEvent : IDomainEvent
    {
        public PersonCreatedEvent()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}