using Domain.Entities;

namespace FoodAutomationSystem.ViewModels
{
    public class FoodMenuViewModel
    {
        public FoodMenu FoodMenu { get; set; }
        public string Date { get; set; }
        public bool IsPast { get; set; }
    }
}
