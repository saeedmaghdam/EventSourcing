using System.Text.Json.Serialization;

namespace EventSourcing.Framework
{
    public interface IEvent
    {
        [JsonIgnore]
        Guid AggregateId { get; set; }
        [JsonIgnore]
        int EventVersion { get; set; }
        void Initialize(Guid aggregateId, int eventVersion);
    }
}