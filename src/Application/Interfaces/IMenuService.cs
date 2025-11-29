using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMenuService : IService<Menu>
    {
        Task<Menu?> GetByDate(DateTime date);
    }
}
