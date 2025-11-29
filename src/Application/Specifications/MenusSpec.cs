using Domain.Entities;

namespace Application.Specifications
{
    public class MenusSpec : Specification<Menu>
    {
        public MenusSpec()
        {
            AddInclude(m => m.FoodMenus);
        }
    }
}
