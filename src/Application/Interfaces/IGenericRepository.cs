using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id, ISpecification<T> spec);
    }
}
