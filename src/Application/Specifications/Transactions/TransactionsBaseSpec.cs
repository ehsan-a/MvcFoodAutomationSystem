using Domain.Entities;

namespace Application.Specifications.Transactions
{
    public abstract class TransactionsBaseSpec : Specification<Transaction>
    {
        protected TransactionsBaseSpec()
        {
            AddInclude(m => m.User);
        }
    }
}
