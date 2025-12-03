using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodAutomationSystem.ViewModels
{
    public class AdminFoodViewModel
    {
        public IEnumerable<Food> Foods { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int All { get; set; }
        public bool PreviousStatus { get; set; }
        public bool NextStatus { get; set; }
        public SelectList selectListItems { get; set; }
        public string? TitleFilter { get; set; }
    }
}
