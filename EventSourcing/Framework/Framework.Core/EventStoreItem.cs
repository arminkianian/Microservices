using System.ComponentModel;

namespace Framework.Core
{
    public class EventStoreItem
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }

        public DateTime CreatedAt { get; set; }

        [Description("ignore")]
        public int Sequence { get; set; }

        public string Name { get; set; }
        public string Aggregate { get; set; }
        public string AggregateId { get; set; }
    }
}