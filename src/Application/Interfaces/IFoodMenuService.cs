using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFoodMenuService : IService<FoodMenu>
    {
        Task<IEnumerable<FoodMenu>> GetByDateAsync(DateOnly date);
        Task<FoodMenu?> GetByIdTypeDateAsync(int id, FoodType foodType, DateTime date);
    }
}
