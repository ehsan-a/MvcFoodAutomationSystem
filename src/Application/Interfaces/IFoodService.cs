using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFoodService : IService<Food>
    {
        Task<IEnumerable<Food>> GetAllAsync(string? skip, string? take, string? titleFilter, string? typeFilter, CancellationToken cancellationToken);
        Task<IEnumerable<Food>> GetAllAsync(string? titleFilter, string? typeFilter, CancellationToken cancellationToken);

    }
}
