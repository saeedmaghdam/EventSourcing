using EventSourcing.Aggregates;
using EventSourcing.Framework;
using EventSourcing.Services.Framework;

namespace EventSourcing.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISession _session;

        public AccountService(ISession session)
        {
            _session = session;
        }

        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            return await _session.GetAggregateById<Account>(accountId);
        }

        public async Task<Guid> CreateAccountAsync(string name, decimal initialBalance)
        {
            var account = new Account(name, initialBalance);
            await _session.CommitAsync(account);

            return account.Id;
        }

        public async Task<decimal> DepositAsync(Guid accountId, decimal amount)
        {
            var account = await _session.GetAggregateById<Account>(accountId);
            account.Deposit(amount);
            await _session.CommitAsync(account);

            return account.Balance;
        }

        public async Task<decimal> WithdrawAsync(Guid accountId, decimal amount)
        {
            var account = await _session.GetAggregateById<Account>(accountId);
            account.Withdraw(amount);
            await _session.CommitAsync(account);

            return account.Balance;
        }

        public async Task UpdateNameAsync(Guid accountId, string accountName)
        {
            var account = await _session.GetAggregateById<Account>(accountId);
            account.UpdateName(accountName);
            await _session.CommitAsync(account);
        }
    }
}