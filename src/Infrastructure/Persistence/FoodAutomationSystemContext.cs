using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class FoodAutomationSystemContext : IdentityDbContext<User>
    {
        public FoodAutomationSystemContext(DbContextOptions<FoodAutomationSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Food { get; set; } = default!;
        public DbSet<FoodMenu> FoodMenu { get; set; } = default!;
        public DbSet<Menu> Menu { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; } = default!;
        public DbSet<Transaction> Transaction { get; set; } = default!;
        public DbSet<AppSetting> AppSetting { get; set; } = default!;
    }
}
