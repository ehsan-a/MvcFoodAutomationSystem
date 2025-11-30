using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FoodAutomationSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodAutomationSystemContext") ?? throw new InvalidOperationException("Connection string 'FoodAutomationSystemContext' not found.")));

builder.Services
    .AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FoodAutomationSystemContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IService<Food>, FoodService>();
builder.Services.AddScoped<IService<Menu>, MenuService>();
builder.Services.AddScoped<IService<FoodMenu>, FoodMenuService>();
builder.Services.AddScoped<IService<Transaction>, TransactionService>();
builder.Services.AddScoped<IService<Reservation>, ReservationService>();

builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IFoodMenuService, FoodMenuService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
