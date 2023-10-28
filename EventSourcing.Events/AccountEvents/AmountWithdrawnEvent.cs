namespace EventSourcing.Events.AccountEvents
{
    public record AmountWithdrawnEvent : EventBase
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public AmountWithdrawnEvent(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
