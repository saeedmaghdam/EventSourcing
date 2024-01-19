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

            @event = new AccountCreatedEvent(typeof(AccountCreatedEvent).FullName!, accountId, "Saed");
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(typeof(AmountDepositedEvent).FullName!, accountId, 200);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(typeof(AmountDepositedEvent).FullName!, accountId, 300);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountWithdrawnEvent(typeof(AmountWithdrawnEvent).FullName!, accountId, 50);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(typeof(AmountDepositedEvent).FullName!, accountId, 100);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new NameChangedEvent(typeof(NameChangedEvent).FullName!, accountId, "Saeed");
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountDepositedEvent(typeof(AmountDepositedEvent).FullName!, accountId, 500);
            @event.Initialize(accountId, version++);
            events.Add(@event);

            @event = new AmountWithdrawnEvent(typeof(AmountWithdrawnEvent).FullName!, accountId, 100);
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
