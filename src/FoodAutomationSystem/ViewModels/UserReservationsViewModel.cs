using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class UserReservationsViewModel
    {
        public User User { get; set; }
        public List<Reservation> Reservations { get; set; }
        
    }
}
