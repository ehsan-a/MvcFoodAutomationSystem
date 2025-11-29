using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _repository;
        public TransactionService(IGenericRepository<Transaction> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(Transaction transaction)
        {
            await _repository.AddAsync(transaction);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = new TransactionsSpec();
            var item = await _repository.GetByIdAsync(id, spec);
            if (item == null) return;
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var spec = new TransactionsSpec();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            var spec = new TransactionsSpec();
            return await _repository.GetByIdAsync(id, spec);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _repository.UpdateAsync(transaction);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var spec = new TransactionsSpec();
            return await _repository.ExistsAsync(id, spec);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return (await GetAllAsync()).Where(x => x.Type == TransactionType.Reservation).Sum(x => x.Amount);
        }
        public async Task<double> GetSuccessRateAsync()
        {
            var total = (await GetAllAsync()).Count();
            var success = (await GetAllAsync()).Where(x => x.Status == TransactionStatus.Success).Count();
            return (success * 100) / total;
        }
        public async Task<IEnumerable<Transaction>> GetFailedPaymentsAsync()
        {
            return (await GetAllAsync()).Where(x => x.Status == TransactionStatus.Failed);
        }
        public async Task<decimal> GetUserBalanceAsync(string id)
        {
            return (await GetAllAsync()).Where(x => x.UserId == id).Sum(x => (x.Type == TransactionType.Reservation ? -x.Amount : x.Amount));
        }
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
        {
            return (await GetAllAsync()).Where(x => x.UserId == userId).OrderByDescending(x => x.Date);
        }
    }
}
