namespace EventSourcing.Events.AccountEvents
{
    public record AmountDepositedEvent(string TypeFullName, Guid AccountId, decimal Amount) : EventBase(TypeFullName);
}
