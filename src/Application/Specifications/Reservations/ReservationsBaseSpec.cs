using Domain.Entities;

namespace Application.Specifications.Reservations
{
    public abstract class ReservationsBaseSpec : Specification<Reservation>
    {
        protected ReservationsBaseSpec()
        {
            AddInclude(m => m.User);
            AddInclude(m => m.Transaction);
            AddInclude(m => m.FoodMenu.Menu);
            AddInclude(m => m.FoodMenu.Food);
        }
    }
}
