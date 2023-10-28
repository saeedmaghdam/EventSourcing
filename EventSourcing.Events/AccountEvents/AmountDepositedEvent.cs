namespace EventSourcing.Events.AccountEvents
{
    public record AmountDepositedEvent : EventBase
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public AmountDepositedEvent(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
