using EventSourcing.Framework;

namespace EventSourcing
{
    public abstract record class EventBase : IEvent
    {
        public Guid AggregateId { get; set; }
        public int EventVersion { get; set; }

        public void Initialize(Guid aggregateId, int eventVersion)
        {
            AggregateId = aggregateId;
            EventVersion = eventVersion;
        }
    }
}
