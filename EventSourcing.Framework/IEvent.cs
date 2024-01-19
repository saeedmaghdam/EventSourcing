using System.Text.Json.Serialization;

namespace EventSourcing.Framework
{
    public interface IEvent
    {
        [JsonIgnore]
        Guid AggregateId { get; }
        [JsonIgnore]
        int EventVersion { get; }
        [JsonIgnore]
        string TypeFullName { get; init; }

        void Initialize(Guid aggregateId, int eventVersion);
    }
}
