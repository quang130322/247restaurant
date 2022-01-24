using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        public FoodServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Food> GetFoodsByCate(int cateId)
        {
            return _unitOfWork.FoodRepository.GetQuery().Where(x=>x.Categories.Any(c=>c.Id == cateId)).ToList();
        }
    }
}
