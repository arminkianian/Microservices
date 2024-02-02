using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TransactionalEvent.Domain.People;

namespace TransactionalEvent.Dal
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<OutBoxEvent> OutBoxEvents { get; set; }


        public override int SaveChanges()
        {
            List<BaseEntity> modifiedEntities = GetModifiedEntities();
            AddEventsToOutBox(modifiedEntities);
            return base.SaveChanges();
        }

        private void AddEventsToOutBox(List<BaseEntity> modifiedEntities)
        {
            foreach (var entity in modifiedEntities)
            {
                var events = entity.GetEvents();

                foreach (var domainEvent in events)
                {
                    OutBoxEvents.Add(new OutBoxEvent
                    {
                        EventDate = DateTime.Now,
                        EventData = JsonConvert.SerializeObject(domainEvent),
                        EventType = domainEvent.GetType().FullName,
                        StreamId = entity.Id.ToString(),
                        StreamName = entity.GetType().Name
                    });
                }
            }
        }

        private List<BaseEntity> GetModifiedEntities()
        {
            return ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State != EntityState.Detached)
                .Select(x => x.Entity)
                .Where(x => x.GetEvents().Any())
                .ToList();
        }
    }
}
