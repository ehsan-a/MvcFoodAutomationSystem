using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class UserWalletViewModel
    {
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal WalletBalance { get; set; }
    }
}
