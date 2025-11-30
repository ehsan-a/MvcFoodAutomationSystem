using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Menus
{
    public abstract class MenuBaseSpec : Specification<Menu>
    {
        protected MenuBaseSpec()
        {
            AddInclude(m => m.FoodMenus);
        }
    }
}
