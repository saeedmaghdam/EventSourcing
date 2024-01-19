using EventSourcing.Framework;

namespace EventSourcing
{
    public abstract record class EventBase : IEvent
    {
        public Guid AggregateId { get; private set; }
        public int EventVersion { get; private set; }
        public string TypeFullName { get; init; }

        protected EventBase(string typeFullName)
        {
            TypeFullName = typeFullName;
        }

        public void Initialize(Guid aggregateId, int eventVersion)
        {
            AggregateId = aggregateId;
            EventVersion = eventVersion;
        }
    }
}
