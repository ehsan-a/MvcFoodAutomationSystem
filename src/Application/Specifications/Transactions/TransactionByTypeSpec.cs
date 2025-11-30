using Domain.Entities;

namespace Application.Specifications.Transactions
{
    public class TransactionByTypeSpec : TransactionsBaseSpec
    {
        public TransactionByTypeSpec(TransactionType type)
        {
            Criteria = x => x.Type == type;
        }
    }
}
