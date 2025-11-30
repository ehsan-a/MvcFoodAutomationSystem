using Domain.Entities;

namespace Application.Specifications.FoodMenus
{
    public abstract class FoodMenuBaseSpec : Specification<FoodMenu>
    {
        protected FoodMenuBaseSpec()
        {
            AddInclude(x => x.Food);
            AddInclude(x => x.Menu);
        }
    }
}
