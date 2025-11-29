using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TodayReservations { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveStudents { get; set; }
        public int WeeklyReservations { get; set; }
        public double SuccessRate { get; set; }
        public int FailedPayments { get; set; }
        public List<Reservation> TodayReservationsList { get; set; }
        public List<WeeklyData> WeeklyData { get; set; }
        public List<FoodTypeData> FoodTypeData { get; set; }

    }
    public class FoodTypeData
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Color { get; set; }
    }
    public class WeeklyData
    {
        public string Name { get; set; }
        public double Value { get; set; }

    }
}
