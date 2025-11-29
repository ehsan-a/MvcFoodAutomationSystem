using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FoodAutomationSystemContext _context;
        public GenericRepository(FoodAutomationSystemContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec)
        {
            var queryable = _context.Set<T>().AsQueryable();
            queryable = SpecificationEvaluator<T>.GetQuery(queryable, spec);
            return await queryable.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, ISpecification<T> spec)
        {
            var queryable = _context.Set<T>().AsQueryable();
            queryable = SpecificationEvaluator<T>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id, ISpecification<T> spec)
        {
            var entity = await GetByIdAsync(id, spec);
            return entity != null;
        }
    }
}
