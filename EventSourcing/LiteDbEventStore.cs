using EventSourcing.Domains;
using EventSourcing.Framework;
using LiteDB;
using System.Reflection;
using System.Text.Json;

namespace EventSourcing
{
    public class LiteDbEventStore : IEventStore
    {
        private readonly string _databasePath;

        public LiteDbEventStore()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            if (!Directory.Exists(Path.Combine(exePath, "data")))
                Directory.CreateDirectory(Path.Combine(exePath, "data"));

            _databasePath = Path.Combine(exePath, "data", "database.db");
        }

        public Task<List<IEvent>> GetEventsAsync(Guid aggregateId)
        {
            using (var db = new LiteDatabase(_databasePath))
            {
                var collection = db.GetCollection<Event>("events");

                var dbEvents = collection.Query().Where(x => x.Payload.AggregateId == aggregateId).ToList();
                var events = dbEvents.Select(x => x.Payload);

                return Task.FromResult(events.ToList());
            }
        }

        public Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events)
        {
            var databaseEvents = events.Select(x => new Event
            {
                CreatedAt = DateTime.UtcNow,
                Payload = x
            });
            using (var db = new LiteDatabase(_databasePath))
            {
                var collection = db.GetCollection<Event>("events");
                collection.InsertBulk(databaseEvents);
            }

            return Task.CompletedTask;
        }
    }
}
