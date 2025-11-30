using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMenuService : IService<Menu>
    {
        Task<Menu?> GetByDateAsync(DateTime date, CancellationToken cancellationToken);
    }
}
