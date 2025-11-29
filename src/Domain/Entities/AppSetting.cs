namespace Domain.Entities
{
    public class AppSetting
    {
        public int Id { get; set; }
        public TimeSpan ReservationDeadline { get; set; }
        public TimeSpan CancellationDeadline { get; set; }
        public TimeOnly BreakfastStart { get; set; }
        public TimeOnly BreakfastEnd { get; set; }
        public TimeOnly LunchStart { get; set; }
        public TimeOnly LunchEnd { get; set; }
        public int MaximumDailyCapacity { get; set; }
        public decimal MinimumWalletBalance { get; set; }
        public decimal MaximumTopUpAmount { get; set; }
    }
}
