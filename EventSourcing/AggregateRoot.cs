using EventSourcing.Extensions;
using EventSourcing.Framework;

namespace EventSourcing
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        protected List<IEvent> Events { get; private set; } = new List<IEvent>();

        protected AggregateRoot() { }

        protected AggregateRoot(Guid id)
        {
            Id = id;

            Events = new List<IEvent>();
        }

        protected AggregateRoot(Guid id, int version) : this(id) { }

        public IEnumerable<IEvent> GetEvents() => Events.ToList();

        protected void AddEvent(IEvent @event)
        {
            Version++;
            @event.Initialize(Id, Version);
            Events.Add(@event);
            ApplyEvent(@event);
        }

        public void ReplayEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events.OrderBy(x=> x.EventVersion))
            {
                ApplyEvent(@event);
                Version = @event.EventVersion;
            }
        }

        private void ApplyEvent(IEvent @event)
        {
            this.Invoke("Apply", @event);
        }
    }
}
