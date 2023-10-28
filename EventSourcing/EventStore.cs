using EventSourcing.Framework;

namespace EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> Events = new Dictionary<Guid, List<IEvent>>();

        public Task<List<IEvent>> GetEventsAsync(Guid aggregateId)
        {
            if (!Events.TryGetValue(aggregateId, out var events))
                return Task.FromResult(new List<IEvent>());

            return Task.FromResult(events);
        }

        public Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events)
        {
            if (!Events.TryGetValue(aggregateId, out _))
                Events[aggregateId] = new List<IEvent>();

            Events[aggregateId].AddRange(events);

            return Task.CompletedTask;
        }
    }
}
