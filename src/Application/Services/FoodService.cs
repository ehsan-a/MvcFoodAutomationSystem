using Application.Specifications;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly IGenericRepository<Food> _repository;

        public FoodService(IGenericRepository<Food> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(Food food)
        {
            await _repository.AddAsync(food);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = new FoodsSpec();
            var item = await _repository.GetByIdAsync(id, spec);
            if (item == null) return;
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            var spec = new FoodsSpec();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            var spec = new FoodsSpec();
            return await _repository.GetByIdAsync(id, spec);
        }

        public async Task UpdateAsync(Food food)
        {
            await _repository.UpdateAsync(food);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var spec = new FoodsSpec();
            return await _repository.ExistsAsync(id, spec);
        }
    }
}
