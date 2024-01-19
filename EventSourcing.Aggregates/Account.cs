using EventSourcing.Events.AccountEvents;

namespace EventSourcing.Aggregates
{
    public class Account : AggregateRoot
    {
        public string Name { get; protected set; }
        public decimal Balance { get; protected set; }

        public Account() : base() { }

        public Account(Guid id) : base(id) { }

        public Account(string name, decimal initialBalance) : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            AddEvent(new AccountCreatedEvent(typeof(AccountCreatedEvent).FullName!, Id, name));

            if (initialBalance > 0)
                AddEvent(new InitialBalanceSetEvent(typeof(InitialBalanceSetEvent).FullName!, Id, initialBalance));
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            AddEvent(new NameChangedEvent(typeof(NameChangedEvent).FullName!, Id, name));
        }

        public void Deposit(decimal balance)
        {
            if (balance <= 0)
                throw new ArgumentOutOfRangeException(nameof(balance));

            AddEvent(new AmountDepositedEvent(typeof(AmountDepositedEvent).FullName!, Id, balance));
        }

        public void Withdraw(decimal balance)
        {
            if (balance <= 0)
                throw new ArgumentOutOfRangeException(nameof(balance));

            AddEvent(new AmountWithdrawnEvent(typeof(AmountWithdrawnEvent).FullName!, Id, balance));
        }

        internal void Apply(AccountCreatedEvent @event)
        {
            Id = @event.AccountId;
            Name = @event.AccountName;
        }

        internal void Apply(InitialBalanceSetEvent @event)
        {
            Balance = @event.Amount;
        }

        internal void Apply(NameChangedEvent @event)
        {
            Name = @event.AccountName;
        }

        internal void Apply(AmountDepositedEvent @event)
        {
            Balance += @event.Amount;
        }

        internal void Apply(AmountWithdrawnEvent @event)
        {
            Balance -= @event.Amount;
        }
    }
}
