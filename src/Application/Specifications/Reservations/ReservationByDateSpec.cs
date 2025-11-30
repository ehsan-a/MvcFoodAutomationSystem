using Domain.Entities;

namespace Application.Specifications.Reservations
{
    public class ReservationByDateSpec : ReservationsBaseSpec
    {
        public ReservationByDateSpec(DateTime date)
        {
            Criteria = x => x.Date.Date == date.Date;
        }
    }
}
