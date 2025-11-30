using Domain.Entities;

namespace Application.Specifications.Menus
{
    public class MenuByDateSpec : MenuBaseSpec
    {
        public MenuByDateSpec(DateTime date)
        {
            Criteria = x => x.WeekStartDate <= DateOnly.FromDateTime(date) && x.WeekStartDate.AddDays(6) >= DateOnly.FromDateTime(date);
        }
    }
}
