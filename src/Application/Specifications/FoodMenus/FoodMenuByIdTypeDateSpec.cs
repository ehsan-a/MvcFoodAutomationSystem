using Domain.Entities;

namespace Application.Specifications.FoodMenus
{
    public class FoodMenuByIdTypeDateSpec : FoodMenuBaseSpec
    {
        public FoodMenuByIdTypeDateSpec(int id, FoodType foodType, DateTime date)
        {
            Criteria = m =>
     m.MenuId == id &&
     m.Food.Type == foodType &&
     m.DayOfWeek == date.DayOfWeek;
        }
    }
}
