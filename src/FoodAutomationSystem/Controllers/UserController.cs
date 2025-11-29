using Domain.Entities;
using FoodAutomationSystem.ViewModels;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodAutomationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IFoodMenuService _foodMenuService;
        private readonly IReservationService _reservationService;
        private readonly IMenuService _menuService;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, ITransactionService transactionService, IFoodMenuService foodMenuService, IReservationService reservationService, IMenuService menuService)
        {
            _userManager = userManager;
            _transactionService = transactionService;
            _foodMenuService = foodMenuService;
            _reservationService = reservationService;
            _menuService = menuService;
        }
        public async Task<IActionResult> Overview()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                DateOnly tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
                var model = new UserOverviewViewModel
                {
                    User = user,
                    TomorrowsFoodMenus = (await _foodMenuService.GetByDateAsync(tomorrow)).ToList(),
                    UpcomingReservations = (await _reservationService.GetUpcomingReservationsAsync(user.Id)).ToList(),
                    WalletBalance = await _transactionService.GetUserBalanceAsync(user.Id)
                };
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> WeeklyMenu(string date)
        {
            var selectedDate = string.IsNullOrEmpty(date) ? DateTime.Now.AddDays(1).Date : DateTime.Parse(date);
            var menu = await _menuService.GetByDate(selectedDate);
            var dates = Enumerable.Range(0, 7).Select(i => menu.WeekStartDate.AddDays(i).ToString("yyyy-MM-dd")).ToList();
            FoodMenu breakfast = null;
            var query = await _foodMenuService.GetByIdTypeDateAsync(menu.Id, FoodType.Breakfast, selectedDate);
            if (query != null) breakfast = query;
            FoodMenu lunch = null;
            query = await _foodMenuService.GetByIdTypeDateAsync(menu.Id, FoodType.Lunch, selectedDate);
            if (query != null) lunch = query;
            bool deadlinePassed = DateTime.Now > selectedDate;
            var vm = new WeeklyMenuViewModel
            {
                SelectedDate = selectedDate.ToString("yyyy-MM-dd"),
                Dates = dates,
                Breakfast = breakfast,
                Lunch = lunch,
                IsDeadlinePassed = deadlinePassed
            };
            return View(vm);
        }
        public async Task<IActionResult> Wallet()
        {

            var user = await _userManager.GetUserAsync(User);
            var vm = new UserWalletViewModel
            {
                User = user,
                Transactions = (await _transactionService.GetByUserIdAsync(user.Id)).ToList(),
                WalletBalance = await _transactionService.GetUserBalanceAsync(user.Id)
            };
            return View(vm);
        }

        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new UserReservationsViewModel
            {
                User = user,
                Reservations = (await _reservationService.GetByUserId(user.Id)).ToList()

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ReservationFlow(int foodMenuId, string date)
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new ReservationFlowViewModel
            {
                User = user,
                FoodMenu = await _foodMenuService.GetByIdAsync(foodMenuId),
                Date = DateTime.Parse(date),
                WalletBalance = await _transactionService.GetUserBalanceAsync(user.Id)
            };
            return View(vm);
        }

        public async Task<IActionResult> FoodDetails(int id)
        {
            var foodMenu = await _foodMenuService.GetByIdAsync(id);
            var date = foodMenu.Menu.WeekStartDate.AddDays((int)foodMenu.DayOfWeek);
            var vm = new FoodMenuViewModel
            {
                FoodMenu = foodMenu,
                Date = date.ToString("yyyy-MM-dd"),
                IsPast = DateTime.Now.DayOfYear >= date.DayOfYear,
            };
            return View(vm);
        }

        public async Task<IActionResult> QRTicket(int id)
        {
            var vm = new QRTicketViewModel
            {
                Reservation = await _reservationService.GetByIdAsync(id),
            };
            return View(vm);
        }


        public async Task<IActionResult> ReservationSuccess(int id)
        {
            var vm = new QRTicketViewModel
            {
                Reservation = await _reservationService.GetByIdAsync(id),
            };
            return View(vm);
        }
    }
}
