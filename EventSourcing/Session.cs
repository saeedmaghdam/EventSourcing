using EventSourcing.Framework;

namespace EventSourcing
{
    public class Session : ISession
    {
        private readonly IEventStore _eventStore;

        public Session(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task CommitAsync(IAggregateRoot aggregate)
        {
            var events = aggregate.GetEvents();
            await _eventStore.SaveEventsAsync(aggregate.Id, events);
        }

        public async Task<TAggregate> GetAggregateById<TAggregate>(Guid aggregateId)
        {
            var events = await _eventStore.GetEventsAsync(aggregateId);

            var type = typeof(TAggregate);
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("ReplayEvents");
            methodInfo!.Invoke(instance, new object[] { events });

            return (TAggregate)instance!;
        }
    }
}
