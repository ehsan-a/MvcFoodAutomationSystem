using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class ReservationsSpec : Specification<Reservation>
    {
        public ReservationsSpec()
        {
            AddInclude(m => m.User);
            AddInclude(m => m.Transaction);
            AddInclude("FoodMenu.Food");
            AddInclude("FoodMenu.Menu");
        }
    }
}
