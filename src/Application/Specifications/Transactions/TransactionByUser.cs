using Domain.Entities;

namespace Application.Specifications.Transactions
{
    public class TransactionByUser : TransactionsBaseSpec
    {
        public TransactionByUser(string userId)
        {
            Criteria = x => x.UserId == userId;
        }
    }
}
