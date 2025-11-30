using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFoodMenuService : IService<FoodMenu>
    {
        Task<IEnumerable<FoodMenu>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken);
        Task<FoodMenu?> GetByIdTypeDateAsync(int id, FoodType foodType, DateTime date, CancellationToken cancellationToken);
    }
}
