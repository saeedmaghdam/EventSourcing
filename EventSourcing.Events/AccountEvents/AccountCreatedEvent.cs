namespace EventSourcing.Events.AccountEvents
{
    public record AccountCreatedEvent : EventBase
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }

        public AccountCreatedEvent(Guid accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}