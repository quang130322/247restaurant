using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface IFoodServices : IBaseService<Food>
    {
        IEnumerable<Food> GetFoodsByCate(int cateId);

        IEnumerable<Food> GetSimilarFood(int foodId);
    }
}
