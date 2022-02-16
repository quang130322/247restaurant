using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface IFoodServices : IBaseService<Food>
    {
        IEnumerable<Food> GetSimilarFood(int foodId, bool canLoadDelete = false);
        IEnumerable<Food> GetFoodByCategory(int cateId, bool canLoadDelete = false);
    }
}
