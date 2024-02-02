namespace TransactionalEvent
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _events;
        public Guid Id { get; set; }


        public BaseEntity()
        {
            _events = new List<IDomainEvent>();
        }

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<IDomainEvent> GetEvents()
        {
            return _events.AsEnumerable();
        }
    }
}
