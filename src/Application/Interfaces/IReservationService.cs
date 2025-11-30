using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservationService : IService<Reservation>
    {
        Task<IEnumerable<Reservation>> GetTodayReservationsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetWeeklyReservationsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetByUserId(string userId, CancellationToken cancellationToken);
    }
}
