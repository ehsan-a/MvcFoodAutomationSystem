using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionService : IService<Transaction>
    {
        Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken);
        Task<double> GetSuccessRateAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetFailedPaymentsAsync(CancellationToken cancellationToken);
        Task<decimal> GetUserBalanceAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}
