using EventSourcing.Aggregates;

namespace EventSourcing.Services.Framework
{
    public interface IAccountService
    {
        Task<Account> GetByIdAsync(Guid accountId);
        Task<Guid> CreateAccountAsync(string name, decimal initialBalance);
        Task<decimal> DepositAsync(Guid accountId, decimal amount);
        Task<decimal> WithdrawAsync(Guid accountId, decimal amount);
        Task UpdateNameAsync(Guid accountId, string accountName);
    }
}
