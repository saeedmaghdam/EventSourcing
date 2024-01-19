namespace EventSourcing.Events.AccountEvents
{
    public record NameChangedEvent(string TypeFullName, Guid AccountId, string AccountName) : EventBase(TypeFullName);
}
