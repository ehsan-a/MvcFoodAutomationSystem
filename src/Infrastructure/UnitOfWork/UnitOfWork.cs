using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodAutomationSystemContext _context;

        public IGenericRepository<Food> Foods { get; }
        public IGenericRepository<Menu> Menus { get; }
        public IGenericRepository<FoodMenu> FoodMenus { get; }
        public IGenericRepository<Reservation> Reservations { get; }
        public IGenericRepository<Transaction> Transactions { get; }

        public UnitOfWork(FoodAutomationSystemContext context)
        {
            _context = context;

            Foods = new GenericRepository<Food>(_context);
            Menus = new GenericRepository<Menu>(_context);
            FoodMenus = new GenericRepository<FoodMenu>(_context);
            Reservations = new GenericRepository<Reservation>(_context);
            Transactions = new GenericRepository<Transaction>(_context);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
