using Application.Interfaces;
using Application.Specifications.Transactions;
using Domain.Entities;
namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new TransactionsSpec();
            var item = await _unitOfWork.Transactions.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Transactions.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionsSpec();
            return await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new TransactionsSpec();
            return await _unitOfWork.Transactions.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            _unitOfWork.Transactions.Update(transaction);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new TransactionsSpec();
            return await _unitOfWork.Transactions.ExistsAsync(id, spec, cancellationToken);
        }

        public async Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionByTypeSpec(TransactionType.Reservation);
            return (await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken))
                .Where(x => x.Status == TransactionStatus.Success).Sum(x => x.Amount);
        }
        public async Task<double> GetSuccessRateAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionsSpec();
            var total = (await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken)).Count();
            var spec2 = new TransactionByStatusSpec(TransactionStatus.Success);
            var success = (await _unitOfWork.Transactions.GetAllAsync(spec2, cancellationToken)).Count();
            return success == 0 ? 0 : (success * 100) / total;
        }
        public async Task<IEnumerable<Transaction>> GetFailedPaymentsAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionByStatusSpec(TransactionStatus.Failed);
            return await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken);
        }
        public async Task<decimal> GetUserBalanceAsync(string userId, CancellationToken cancellationToken)
        {
            var spec = new TransactionByUser(userId);
            return (await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken))
                .Sum(x => x.Type == TransactionType.Reservation ? -x.Amount : x.Amount);
        }
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var spec = new TransactionByUser(userId);
            return (await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken))
                .OrderByDescending(x => x.Date);
        }
    }
}
