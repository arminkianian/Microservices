namespace TransactionalEvent.Domain
{
    internal class BirthDateUpdatedEvent : IDomainEvent
    {
        public BirthDateUpdatedEvent()
        {
        }

        public Guid Id { get; set; }
        public DateTime BirthDate { get; set; }
    }
}