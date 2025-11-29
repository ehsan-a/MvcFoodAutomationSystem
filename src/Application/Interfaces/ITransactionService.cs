using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionService : IService<Transaction>
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<double> GetSuccessRateAsync();
        Task<IEnumerable<Transaction>> GetFailedPaymentsAsync();
        Task<decimal> GetUserBalanceAsync(string id);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
    }
}
