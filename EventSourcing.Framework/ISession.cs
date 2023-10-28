namespace EventSourcing.Framework
{
    public interface ISession
    {
        Task CommitAsync(IAggregateRoot aggregate);
        Task<TAggregate> GetAggregateById<TAggregate>(Guid aggregateId);
    }
}
