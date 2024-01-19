namespace EventSourcing.Events.AccountEvents
{
    public record AccountCreatedEvent(string TypeFullName, Guid AccountId, string AccountName) : EventBase(TypeFullName);
}
