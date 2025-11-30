using Application.Interfaces;
using Application.Specifications.Menus;
using Domain.Entities;

namespace Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Menu menu, CancellationToken cancellationToken)
        {
            await _unitOfWork.Menus.AddAsync(menu, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new MenusSpec();
            var item = await _unitOfWork.Menus.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Menus.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Menu>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new MenusSpec();
            return await _unitOfWork.Menus.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Menu?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new MenusSpec();
            return await _unitOfWork.Menus.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Menu menu, CancellationToken cancellationToken)
        {
            _unitOfWork.Menus.Update(menu);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new MenusSpec();
            return await _unitOfWork.Menus.ExistsAsync(id, spec, cancellationToken);
        }
        public async Task<Menu?> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            var spec = new MenuByDateSpec(date);
            return (await _unitOfWork.Menus.GetAllAsync(spec, cancellationToken)).FirstOrDefault();
        }
    }
}
