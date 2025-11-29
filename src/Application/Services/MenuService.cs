using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _repository;
        public MenuService(IGenericRepository<Menu> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(Menu menu)
        {
            await _repository.AddAsync(menu);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = new MenusSpec();
            var item = await _repository.GetByIdAsync(id, spec);
            if (item == null) return;
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            var spec = new MenusSpec();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            var spec = new MenusSpec();
            return await _repository.GetByIdAsync(id, spec);
        }

        public async Task UpdateAsync(Menu menu)
        {
            await _repository.UpdateAsync(menu);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var spec = new MenusSpec();
            return await _repository.ExistsAsync(id, spec);
        }
        public async Task<Menu?> GetByDate(DateTime date)
        {
            return (await GetAllAsync()).FirstOrDefault(x => x.WeekStartDate.DayOfYear <= date.DayOfYear && x.WeekStartDate.AddDays(6).DayOfYear >= date.DayOfYear);
        }
    }
}
