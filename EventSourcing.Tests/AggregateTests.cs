using EventSourcing.Aggregates;
using EventSourcing.Events.AccountEvents;
using EventSourcing.Framework;

namespace EventSourcing.Tests
{
    public class AggregateTests
    {
        [Fact]
        public void ReplayEvents_ShouldCreateAValidStateOfAggregate()
        {
            var events = new List<IEvent>();
            var @event = default(IEvent);

            var accountId = Guid.NewGuid();
            var version = 1;

            @event = new AccountCreatedEvent(accountId, "Saed");
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(accountId, 200);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(accountId, 300);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountWithdrawnEvent(accountId, 50);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(accountId, 100);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new NameChangedEvent(accountId, "Saeed");
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(accountId, 500);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountWithdrawnEvent(accountId, 100);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            var accountAggregate = new Account();
            accountAggregate.ReplayEvents(events);

            Assert.Equal(950, accountAggregate.Balance);
            Assert.Equal("Saeed", accountAggregate.Name);
            Assert.Equal(accountId, accountAggregate.Id);
            Assert.Equal(8, accountAggregate.Version);
        }
    }
}