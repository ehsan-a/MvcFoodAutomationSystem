using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodAutomationSystem.ViewModels
{
    public class MenuManagementViewModel
    {
        public string SearchQuery { get; set; }
        public SelectList WeeklyMenuOptions { get; set; }
        public int SelectedWeeklyMenuId { get; set; }
        public Menu SelectedWeeklyMenu { get; set; }
        public List<string> Days { get; set; }
        public List<string> Dates { get; set; }
        public List<Food> AllFoods { get; set; }
    }
}
