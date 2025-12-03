using Application.Interfaces;
using Domain.Entities;
using FoodAutomationSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.Threading;

namespace FoodAutomationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IReservationService _reservationService;
        private readonly ITransactionService _transactionService;
        private readonly IFoodService _foodService;
        private readonly IService<Menu> _menuService;
        public AdminController(UserManager<User> userManager, IReservationService reservationService, ITransactionService transactionService, IFoodService foodService, IService<Menu> menuService)
        {
            _userManager = userManager;
            _reservationService = reservationService;
            _transactionService = transactionService;
            _foodService = foodService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Dashboard(CancellationToken cancellationToken)
        {
            var vm = new AdminDashboardViewModel
            {
                TodayReservations = (await _reservationService.GetTodayReservationsAsync(cancellationToken)).Count(),
                TotalRevenue = await _transactionService.GetTotalRevenueAsync(cancellationToken),
                ActiveStudents = _userManager.Users.Count(),
                WeeklyReservations = (await _reservationService.GetWeeklyReservationsAsync(cancellationToken)).Count(),
                SuccessRate = await _transactionService.GetSuccessRateAsync(cancellationToken),
                FailedPayments = (await _transactionService.GetFailedPaymentsAsync(cancellationToken)).Count(),
                TodayReservationsList = (await _reservationService.GetTodayReservationsAsync(cancellationToken)).ToList(),
                WeeklyData = (await _reservationService.GetWeeklyReservationsAsync(cancellationToken)).GroupBy(x => x.Date)
                .Select(x => new WeeklyData { Name = x.Key.DayOfWeek.ToString(), Value = x.Count() }).ToList(),
                FoodTypeData = [
                    new FoodTypeData{Name="Breakfast",Value=(await _foodService.GetAllAsync(cancellationToken)).Count(x=>x.Type==FoodType.Breakfast),Color="#3b82f6"},
                    new FoodTypeData{Name="Luanch",Value=(await _foodService.GetAllAsync(cancellationToken)).Count(x=>x.Type==FoodType.Lunch),Color="#ef4444"}
                    ],
            };
            return View(vm);
        }

        public async Task<IActionResult> MenuManagement(string SelectedWeeklyMenuId, CancellationToken cancellationToken)
        {
            if (SelectedWeeklyMenuId.IsNullOrEmpty())
            {
                if ((await _menuService.GetAllAsync(cancellationToken)).Any())
                    SelectedWeeklyMenuId = (await _menuService.GetAllAsync(cancellationToken)).FirstOrDefault(x => x.WeekStartDate.DayOfYear <= DateTime.Now.AddDays(1).DayOfYear && x.WeekStartDate.AddDays(6).DayOfYear >= DateTime.Now.AddDays(1).DayOfYear).Id.ToString();
                else return View(new MenuManagementViewModel());
            }
            var menu = await _menuService.GetByIdAsync(int.Parse(SelectedWeeklyMenuId), cancellationToken);

            var vm = new MenuManagementViewModel
            {
                Dates = Enumerable.Range(0, 7)
                .Select(i => menu.WeekStartDate.AddDays(i).ToString("yyyy-MM-dd"))
                .ToList(),
                Days = Enum.GetValues(typeof(DayOfWeek))
               .Cast<DayOfWeek>()
               .Select(d => d.ToString())
               .ToList(),
                SearchQuery = "",
                SelectedWeeklyMenu = menu,
                WeeklyMenuOptions = new SelectList(await _menuService.GetAllAsync(cancellationToken), "Id", "WeekStartDate"),
                SelectedWeeklyMenuId = int.Parse(SelectedWeeklyMenuId),
                AllFoods = (await _foodService.GetAllAsync(cancellationToken)).ToList(),
            };
            return View(vm);
        }

        public async Task<IActionResult> UserManagement(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Include(x => x.Reservations)
                .ToListAsync();

            var vm = new List<UserManagementViewModel>();

            foreach (var user in users)
            {
                var balance = await _transactionService.GetUserBalanceAsync(user.Id, cancellationToken);

                vm.Add(new UserManagementViewModel
                {
                    User = user,
                    WalletBalance = balance
                });
            }
            return View(vm);

        }
        public async Task<IActionResult> FoodManagement(string? skip, string? take, string? titleFilter, string? typeFilter, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(skip))
            {
                skip = "0";
                take = "5";
            }
            if (int.Parse(skip) < 0) skip = "0";
            var foods = await _foodService.GetAllAsync(skip, take, titleFilter, typeFilter, cancellationToken);
            int skipNumber = int.Parse(skip);
            int takeNumber = int.Parse(take);
            var vm = new AdminFoodViewModel
            {
                Foods = foods,
                Skip = skipNumber,
                Take = takeNumber,
                All = (await _foodService.GetAllAsync(titleFilter, typeFilter, cancellationToken)).Count(),
                PreviousStatus = skipNumber > 1,
                NextStatus = foods.Count() == takeNumber,
                selectListItems = new SelectList(Enum.GetValues<FoodType>().Select(x => new { Id = (int)x, Title = x.ToString() }), "Id", "Title", typeFilter),
                TitleFilter = titleFilter,
            };

            return View(vm);
        }
        public async Task<IActionResult> ReservationsManagement(CancellationToken cancellationToken)
        {
            return View(await _reservationService.GetAllAsync(cancellationToken));
        }

        public async Task<IActionResult> PaymentManagement(CancellationToken cancellationToken)
        {
            return View(await _transactionService.GetAllAsync(cancellationToken));
        }

        public IActionResult Setting()
        {
            return View();
        }

        public async Task<IActionResult> DailyReservationReport(CancellationToken cancellationToken)
        {
            var vm = new DailyReservationReportViewModel
            {
                SelectedDate = DateTime.Now.AddDays(-1),
                TotalReservations = 0,
                UniqueUsers = 0,
                TotalMeals = 0,
                TotalRevenue = 0,
                FoodSummary = (await _reservationService.GetAllAsync(cancellationToken)).Where(x => x.Date.Date == DateTime.Now.AddDays(-1).Date)
                .GroupBy(x => x.FoodMenu)
                .Select(x => new FoodItemSummaryDto
                {
                    MealName = x.Key.Food.Title,
                    MealType = x.Key.Food.Type.ToString(),
                    Quantity = x.Count(),
                    Revenue = 0,
                    Percentage = 0
                }).ToList()
            };
            return View(vm);
        }
    }
}
