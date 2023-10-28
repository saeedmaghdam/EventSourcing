namespace EventSourcing.Framework
{
    public interface IAggregateRoot
    {
        public Guid Id { get; }
        public int Version { get; }
        IEnumerable<IEvent> GetEvents();
    }
}
