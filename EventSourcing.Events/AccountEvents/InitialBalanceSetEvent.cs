namespace EventSourcing.Events.AccountEvents
{
    public record InitialBalanceSetEvent : EventBase
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public InitialBalanceSetEvent(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
