namespace Application.Specifications.Reservations
{
    public class ReservationByDateRangeSpec : ReservationsBaseSpec
    {
        public ReservationByDateRangeSpec(DateTime startDate, DateTime endDate)
        {
            Criteria = x => x.Date.Date >= startDate && x.Date.Date <= endDate;
        }
    }
}
