using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Foods
{
    public class FoodsSpec : FoodBaseSpec
    {
        public FoodsSpec(string? skip = null, string? take = null, string? titleFilter = null, string? typeFilter = null)
        {
            if (!string.IsNullOrEmpty(skip) && !string.IsNullOrEmpty(take))
            {
                ApplyPaging(int.Parse(skip), int.Parse(take));
            }

            if (!string.IsNullOrEmpty(titleFilter) && string.IsNullOrEmpty(typeFilter))
            {
                AddCriteria(x => x.Title.Contains(titleFilter));
            }
            else if (!string.IsNullOrEmpty(typeFilter) && string.IsNullOrEmpty(titleFilter))
            {
                AddCriteria(x => (int)x.Type == int.Parse(typeFilter));
            }
            else if (!string.IsNullOrEmpty(typeFilter) && !string.IsNullOrEmpty(titleFilter))
            {
                AddCriteria(x => (int)x.Type == int.Parse(typeFilter) && x.Title.Contains(titleFilter));
            }
        }
    }
}
