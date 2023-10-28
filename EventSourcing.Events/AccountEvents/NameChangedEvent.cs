namespace EventSourcing.Events.AccountEvents
{
    public record NameChangedEvent : EventBase
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }

        public NameChangedEvent(Guid accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
