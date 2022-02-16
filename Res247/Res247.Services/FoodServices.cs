using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        public FoodServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Food> GetFoodByCategory(int cateId, bool canLoadDelete = false)
        {
            return _unitOfWork.FoodRepository.GetQuery().Where(x => x.CategoryId == cateId 
                                                            && x.IsDeleted == canLoadDelete).ToList();
        }

        public IEnumerable<Food> GetSimilarFood(int foodId, bool canLoadDelete = false)
        {
            var cate = _unitOfWork.CategoryRepository.GetQuery().FirstOrDefault(x=>x.Foods.Any(f=>f.Id == foodId));
            return _unitOfWork.FoodRepository.GetQuery().Where(x=>x.CategoryId == cate.Id 
                                                            && x.IsDeleted == canLoadDelete 
                                                            && x.Id != foodId).Take(10).ToList();
        }
    }
}
