using Domain.Entities;

namespace Application.Specifications.Transactions
{
    public class TransactionByStatusSpec : TransactionsBaseSpec
    {
        public TransactionByStatusSpec(TransactionStatus status)
        {
            Criteria = x => x.Status == status;
        }
    }
}
