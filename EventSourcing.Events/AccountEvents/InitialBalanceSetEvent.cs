namespace EventSourcing.Events.AccountEvents
{
    public record InitialBalanceSetEvent(string TypeFullName, Guid AccountId, decimal Amount) : EventBase(TypeFullName);
}
