using Domain.Entities;

namespace Application.Specifications.FoodMenus
{
    public class FoodMenuByDateSpec : FoodMenuBaseSpec
    {
        public FoodMenuByDateSpec(DateOnly date)
        {
            Criteria = x => x.Menu.WeekStartDate.AddDays((int)x.DayOfWeek) == date;
        }
    }
}
