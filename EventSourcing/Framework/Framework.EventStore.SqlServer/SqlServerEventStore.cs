using Framework.Core;
using Newtonsoft.Json;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace Framework.EventStore.SqlServer
{
    public class SqlServerEventStore : IEventStore
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
        };

        private readonly string _sqlConnectionString;

        public SqlServerEventStore(IConfiguration configuration)
        {
            _sqlConnectionString = configuration.GetConnectionString("EventStoreDb");
        }

        public async Task<IReadOnlyCollection<EventStoreItem>> GetAll(DateTime? afterDateTime)
        {
            var whereClause = afterDateTime.HasValue ? $"WHERE [CreatedAt] > '{afterDateTime}'" : "";

            var query = $"SELECT * FROM EventStore {whereClause} ORDER BY [CreatedAt],[Version] ASC";

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var events = await sqlConnection.QueryAsync<EventStoreItem>(query.ToString());
                return events.ToList();
            }
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(Guid aggregateRootId, string aggregateName)
        {
            if (aggregateRootId == null) throw new InvalidOperationException("AggregateRootId can not be null");
            if (string.IsNullOrWhiteSpace(aggregateName)) throw new InvalidOperationException("Aggregatename can not be null");

            var query = $"SELECT * FROM EventStore WHERE [AggregateId] = @AggregateId AND [Aggregate] = @AggregateName";

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var events = await sqlConnection.QueryAsync<EventStoreItem>(query.ToString(),
                    new
                    {
                        AggregateId = aggregateRootId,
                        AggregateName = aggregateName
                    });

                var domainEvents = events
                    .Select(TransformEvent)
                    .Where(x => x != null)
                    .ToList()
                    .AsReadOnly();

                return domainEvents;
            }
        }

        private IDomainEvent TransformEvent(EventStoreItem selectedEvent)
        {
            var deserializedObject = JsonConvert.DeserializeObject(selectedEvent.Data, _jsonSerializerSettings);
            var domainEvent = deserializedObject as IDomainEvent;
            return domainEvent;
        }

        public async Task SaveAsync(Guid aggregateId, string aggregateName, int originatingVersion, IReadOnlyCollection<IDomainEvent> events)
        {
            if (events.Count < 1) return;

            string query = $"INSERT INTO EventStore(Id, CreatedAt, Version, Name, AggregateId, Data, Aggregate)" +
                $" VALUES (@Id,@CreatedAt,@Version,@Name,@AggregateId,@Data,@Aggregate)";

            var listOfEvents = events.Select(@event => new
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Aggregate = aggregateName,
                AggregateId = aggregateId,
                Version = ++originatingVersion,
                Data = JsonConvert.SerializeObject(@event, Formatting.Indented, _jsonSerializerSettings),
                Name = @event.GetType().Name,
            });

            using (var sqlConnaction = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnaction.ExecuteAsync(query, listOfEvents);
            }
        }
    }
}
