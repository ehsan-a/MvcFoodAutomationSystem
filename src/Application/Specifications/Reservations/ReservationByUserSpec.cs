namespace Application.Specifications.Reservations
{
    public class ReservationByUserSpec : ReservationsBaseSpec
    {
        public ReservationByUserSpec(string userId)
        {
            Criteria = x => x.UserId == userId;
        }
    }
}
