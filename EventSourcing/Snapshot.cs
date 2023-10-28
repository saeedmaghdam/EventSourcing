using EventSourcing.Framework;

namespace EventSourcing
{
    public class Snapshot : ISnapshot
    {
        private readonly Dictionary<Guid, IAggregateRoot> Snapshots = new Dictionary<Guid, IAggregateRoot>();

        public Task<IAggregateRoot?> GetByIdAsync(Guid aggregateId)
        {
            if (!Snapshots.TryGetValue(aggregateId, out var aggregate))
                return Task.FromResult(default(IAggregateRoot));

            return Task.FromResult(aggregate)!;
        }

        public Task TakeSnapshotAsync(IAggregateRoot aggregate)
        {
            Snapshots[aggregate.Id] = aggregate;

            return Task.CompletedTask;
        }
    }
}
