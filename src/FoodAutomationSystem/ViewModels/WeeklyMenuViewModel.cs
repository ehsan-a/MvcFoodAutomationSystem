using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class WeeklyMenuViewModel
    {
        public string SelectedDate { get; set; }
        public List<string> Dates { get; set; }
        public FoodMenu? Breakfast { get; set; }
        public FoodMenu? Lunch { get; set; }
        public bool IsDeadlinePassed { get; set; }
    }
}
