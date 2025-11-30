using Application.Interfaces;
using Application.Specifications.FoodMenus;
using Domain.Entities;

namespace Application.Services
{
    public class FoodMenuService : IFoodMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodMenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(FoodMenu foodMenu, CancellationToken cancellationToken)
        {
            await _unitOfWork.FoodMenus.AddAsync(foodMenu, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodMenusSpec();
            var item = await _unitOfWork.FoodMenus.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.FoodMenus.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<FoodMenu>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new FoodMenusSpec();
            return await _unitOfWork.FoodMenus.GetAllAsync(spec, cancellationToken);
        }

        public async Task<FoodMenu?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodMenusSpec();
            return await _unitOfWork.FoodMenus.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(FoodMenu foodMenu, CancellationToken cancellationToken)
        {
            _unitOfWork.FoodMenus.Update(foodMenu);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new FoodMenusSpec();
            return await _unitOfWork.FoodMenus.ExistsAsync(id, spec, cancellationToken);
        }
        public async Task<IEnumerable<FoodMenu>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken)
        {
            var spec = new FoodMenuByDateSpec(date);
            return await _unitOfWork.FoodMenus.GetAllAsync(spec, cancellationToken);
        }
        public async Task<FoodMenu?> GetByIdTypeDateAsync(int id, FoodType foodType, DateTime date, CancellationToken cancellationToken)
        {
            var spec = new FoodMenuByIdTypeDateSpec(id, foodType, date);
            return (await _unitOfWork.FoodMenus.GetAllAsync(spec, cancellationToken)).FirstOrDefault();
        }
    }
}
