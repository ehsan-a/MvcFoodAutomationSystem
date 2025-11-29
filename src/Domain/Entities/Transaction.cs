namespace Domain.Entities
{
    public enum TransactionType
    {
        WalletTopUp,
        Reservation
    }
    public enum TransactionStatus
    {
        Success,
        Failed
    }
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
