using Application.Specifications.Foods;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Food food, CancellationToken cancellationToken)
        {
            await _unitOfWork.Foods.AddAsync(food, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodsSpec();
            var item = await _unitOfWork.Foods.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Foods.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Food>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new FoodsSpec();
            return await _unitOfWork.Foods.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Food?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodsSpec();
            return await _unitOfWork.Foods.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Food food, CancellationToken cancellationToken)
        {
            _unitOfWork.Foods.Update(food);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodsSpec();
            return await _unitOfWork.Foods.ExistsAsync(id, spec, cancellationToken);
        }
    }
}
