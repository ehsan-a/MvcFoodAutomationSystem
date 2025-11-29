using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Persistence
{
    public class FoodAutomationSystemContext : IdentityDbContext<User>
    {
        public FoodAutomationSystemContext(DbContextOptions<FoodAutomationSystemContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            var adminRoleId = "role-admin";
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );

            builder.Entity<Transaction>()
           .Property(t => t.Amount)
           .HasPrecision(18, 2);

            builder.Entity<FoodMenu>()
           .Property(t => t.UnitPrice)
           .HasPrecision(18, 2);

            builder.Entity<Food>()
           .Property(t => t.Price)
           .HasPrecision(18, 2);

            builder.Entity<AppSetting>()
           .Property(t => t.MinimumWalletBalance)
           .HasPrecision(18, 2);

            builder.Entity<AppSetting>()
           .Property(t => t.MaximumTopUpAmount)
           .HasPrecision(18, 2);

            var hasher = new PasswordHasher<User>();

            var user1 = new User
            {
                Id = "user-1",
                UserName = "ehsan@example.com",
                NormalizedUserName = "EHSAN@EXAMPLE.COM",
                Email = "ehsan@example.com",
                NormalizedEmail = "EHSAN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Ehsan",
                LastName = "Arefzadeh",
                Status = UserStatus.Active,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user1.PasswordHash = hasher.HashPassword(user1, "password1234");

            var user2 = new User
            {
                Id = "user-2",
                UserName = "ali@example.com",
                NormalizedUserName = "ALI@EXAMPLE.COM",
                Email = "ali@example.com",
                NormalizedEmail = "ALI@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Ali",
                LastName = "Mohammadi",
                Status = UserStatus.Active,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user2.PasswordHash = hasher.HashPassword(user2, "password1234");

            builder.Entity<User>().HasData(user1, user2);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = user1.Id
                }
            );
        }


        public DbSet<Food> Food { get; set; } = default!;
        public DbSet<FoodMenu> FoodMenu { get; set; } = default!;
        public DbSet<Menu> Menu { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; } = default!;
        public DbSet<Transaction> Transaction { get; set; } = default!;
        public DbSet<AppSetting> AppSetting { get; set; } = default!;
    }
}
