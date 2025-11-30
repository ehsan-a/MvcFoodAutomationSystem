namespace Application.Specifications.Reservations
{
    public class ReservationByUserDateSpec : ReservationsBaseSpec
    {
        public ReservationByUserDateSpec(string userId, DateTime date)
        {
            Criteria = x => x.UserId == userId && x.FoodMenu.Menu.WeekStartDate.AddDays((int)x.FoodMenu.DayOfWeek).DayOfYear >= date.DayOfYear;
        }
    }
}
