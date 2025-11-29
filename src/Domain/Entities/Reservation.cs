namespace Domain.Entities
{
    public enum ReservationStatus
    {
        Confirmed,
        Processing,
        Completed,
        Cancelled
    }

    public enum ReservationPaymentStatus
    {
        Success,
        Failed
    }
    public class Reservation
    {
        public int Id { get; set; }
        public int FoodMenuId { get; set; }
        public FoodMenu? FoodMenu { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string QRCode { get; set; } = Guid.NewGuid().ToString();
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        public ReservationStatus Status { get; set; }
        public ReservationPaymentStatus PaymentStatus { get; set; }
    }
}
