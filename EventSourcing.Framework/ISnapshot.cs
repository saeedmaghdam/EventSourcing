namespace EventSourcing.Framework
{
    public interface ISnapshot
    {
        Task<IAggregateRoot?> GetByIdAsync(Guid aggregateId);
        Task TakeSnapshotAsync(IAggregateRoot aggregateRoot);
    }
}
