namespace FoodAutomationSystem.ViewModels
{
    public class DailyReservationReportViewModel
    {
        public DateTime SelectedDate { get; set; }
        public int TotalReservations { get; set; }
        public int UniqueUsers { get; set; }
        public int TotalMeals { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<FoodItemSummaryDto> FoodSummary { get; set; } = new();
    }

    public class FoodItemSummaryDto
    {
        public string MealName { get; set; }
        public string MealType { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
        public double Percentage { get; set; }
    }
}
