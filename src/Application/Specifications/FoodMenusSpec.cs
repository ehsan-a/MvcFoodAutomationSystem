using Domain.Entities;

namespace Application.Specifications
{
    public class FoodMenusSpec : Specification<FoodMenu>
    {
        public FoodMenusSpec()
        {
            AddInclude(m => m.Food);
            AddInclude(m => m.Menu);
        }
    }
}
