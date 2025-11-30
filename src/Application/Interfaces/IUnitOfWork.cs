using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Food> Foods { get; }
        IGenericRepository<Menu> Menus { get; }
        IGenericRepository<FoodMenu> FoodMenus { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Transaction> Transactions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
