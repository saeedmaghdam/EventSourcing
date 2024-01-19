namespace EventSourcing.Events.AccountEvents
{
    public record AmountWithdrawnEvent(string TypeFullName, Guid AccountId, decimal Amount) : EventBase(TypeFullName);
}
