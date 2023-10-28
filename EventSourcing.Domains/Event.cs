using EventSourcing.Framework;

namespace EventSourcing.Domains
{
    public class Event
    {
        public Guid Id { get; private set; }
        public IEvent Payload { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public Event()
        {
            Id = Guid.NewGuid();
        }
    }
}