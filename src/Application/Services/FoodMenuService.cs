using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class FoodMenuService : IFoodMenuService
    {
        private readonly IGenericRepository<FoodMenu> _repository;
        public FoodMenuService(IGenericRepository<FoodMenu> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(FoodMenu foodMenu)
        {
            await _repository.AddAsync(foodMenu);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = new FoodMenusSpec();
            var item = await _repository.GetByIdAsync(id, spec);
            if (item == null) return;
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<FoodMenu>> GetAllAsync()
        {
            var spec = new FoodMenusSpec();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<FoodMenu?> GetByIdAsync(int id)
        {
            var spec = new FoodMenusSpec();
            return await _repository.GetByIdAsync(id, spec);
        }

        public async Task UpdateAsync(FoodMenu foodMenu)
        {
            await _repository.UpdateAsync(foodMenu);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var spec = new FoodMenusSpec();
            return await _repository.ExistsAsync(id, spec);
        }
        public async Task<IEnumerable<FoodMenu>> GetByDateAsync(DateOnly date)
        {
            return (await GetAllAsync()).Where(x => x.Menu.WeekStartDate.AddDays((int)x.DayOfWeek) == date);
        }
        public async Task<FoodMenu?> GetByIdTypeDateAsync(int id, FoodType foodType, DateTime date)
        {
            return (await GetAllAsync()).FirstOrDefault(m => m.MenuId == id && m.Food.Type == foodType && m.DayOfWeek == date.DayOfWeek);
        }
    }
}
