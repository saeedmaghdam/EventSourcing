namespace EventSourcing.Framework
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events);
        Task<List<IEvent>> GetEventsAsync(Guid aggregateId);
    }
}