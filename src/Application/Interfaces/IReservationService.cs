using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservationService : IService<Reservation>
    {
        Task<IEnumerable<Reservation>> GetTodayReservationsAsync();
        Task<IEnumerable<Reservation>> GetWeeklyReservationsAsync();
        Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync(string userId);
        Task<IEnumerable<Reservation>> GetByUserId(string userId);
    }
}
