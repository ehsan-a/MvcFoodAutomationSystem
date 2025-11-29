using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class UserOverviewViewModel
    {
        public User User { get; set; }
        public List<Reservation>? UpcomingReservations { get; set; }
        public List<FoodMenu>? TomorrowsFoodMenus { get; set; }
        public decimal WalletBalance { get; set; }
    }
}
