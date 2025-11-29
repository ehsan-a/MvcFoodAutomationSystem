namespace FoodAutomationSystem.ViewModels
{
    public class ReservationDataViewModel
    {
        public string UserId { get; set; }
        public int FoodMenuId { get; set; }
        public decimal Amount { get; set; }
        public int? TransactionId { get; set; }
    }
}
