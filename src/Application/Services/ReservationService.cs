using Application.Interfaces;
using Application.Specifications.Reservations;
using Domain.Entities;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            await _unitOfWork.Reservations.AddAsync(reservation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new ReservationsSpec();
            var item = await _unitOfWork.Reservations.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Reservations.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new ReservationsSpec();
            return await _unitOfWork.Reservations.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new ReservationsSpec();
            return await _unitOfWork.Reservations.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            _unitOfWork.Reservations.Update(reservation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new ReservationsSpec();
            return await _unitOfWork.Reservations.ExistsAsync(id, spec, cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetTodayReservationsAsync(CancellationToken cancellationToken)
        {
            var spec = new ReservationByDateSpec(DateTime.Now);
            return await _unitOfWork.Reservations.GetAllAsync(spec, cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetWeeklyReservationsAsync(CancellationToken cancellationToken)
        {
            var weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date;
            var weekEnd = weekStart.AddDays(6).Date;
            var spec = new ReservationByDateRangeSpec(weekStart, weekEnd);
            return await _unitOfWork.Reservations.GetAllAsync(spec, cancellationToken);
        }
        public async Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync(string userId, CancellationToken cancellationToken)
        {
            var spec = new ReservationByUserDateSpec(userId, DateTime.Now);
            return await _unitOfWork.Reservations.GetAllAsync(spec, cancellationToken);
        }
        public async Task<IEnumerable<Reservation>> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            var spec = new ReservationByUserSpec(userId);
            return await _unitOfWork.Reservations.GetAllAsync(spec, cancellationToken);
        }
    }
}
