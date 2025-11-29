using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericRepository<Reservation> _repository;
        public ReservationService(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(Reservation reservation)
        {
            await _repository.AddAsync(reservation);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = new ReservationsSpec();
            var item = await _repository.GetByIdAsync(id, spec);
            if (item == null) return;
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            var spec = new ReservationsSpec();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            var spec = new ReservationsSpec();
            return await _repository.GetByIdAsync(id, spec);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            await _repository.UpdateAsync(reservation);

        }
        public async Task<bool> ExistsAsync(int id)
        {
            var spec = new ReservationsSpec();
            return await _repository.ExistsAsync(id, spec);
        }

        public async Task<IEnumerable<Reservation>> GetTodayReservationsAsync()
        {
            var reservations = await GetAllAsync();
            return reservations.Where(x => x.Date.Date == DateTime.Now.Date);
        }

        public async Task<IEnumerable<Reservation>> GetWeeklyReservationsAsync()
        {
            var weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date;
            var weekEnd = weekStart.AddDays(6).Date;
            var reservations = await GetAllAsync();
            return reservations.Where(x => x.Date.Date >= weekStart && x.Date.Date <= weekEnd);
        }
        public async Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync(string userId)
        {
            return (await GetAllAsync()).Where(x => x.UserId == userId && x.FoodMenu.Menu.WeekStartDate.AddDays((int)x.FoodMenu.DayOfWeek).DayOfYear >= DateTime.Now.DayOfYear);
        }
        public async Task<IEnumerable<Reservation>> GetByUserId(string userId)
        {
            return (await GetAllAsync()).Where(x => x.UserId == userId);
        }
    }
}
